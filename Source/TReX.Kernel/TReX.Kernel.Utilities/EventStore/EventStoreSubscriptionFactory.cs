using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using JsonNet.ContractResolvers;
using MediatR;
using Newtonsoft.Json;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public sealed class EventStoreSubscriptionFactory
    {
        private readonly IEventStoreConnection storeConnection;
        private readonly IMediator mediator;
        private readonly EventStoreSettings settings;

        public EventStoreSubscriptionFactory(IEventStoreConnection storeConnection, IMediator mediator, EventStoreSettings settings)
        {
            EnsureArg.IsNotNull(storeConnection);
            EnsureArg.IsNotNull(mediator);
            EnsureArg.IsNotNull(settings);
            this.storeConnection = storeConnection;
            this.mediator = mediator;
            this.settings = settings;
        }

        public async Task<EventStoreCatchUpSubscription> CreateSubscription<T>()
            where T : IBusMessage
        {
            return await this.Subscribe<T>();
        }

        private async Task<EventStoreCatchUpSubscription> Subscribe<T>()
            where T : IBusMessage
        {
            var checkpoint = await this.GetCheckpointFor<T>().Unwrap(c => c.LastProcessedPosition, StreamPosition.Start);
            var topic = TopicFactory.GetTopic(typeof(T));

            return this.storeConnection.SubscribeToStreamFrom(topic, checkpoint, CatchUpSubscriptionSettings.Default, OnEventAppeared<T>, null, OnSubscriptionDropped<T>);
        }

        private async Task OnEventAppeared<T>(EventStoreCatchUpSubscription subscription, ResolvedEvent @event)
            where T : IBusMessage
        {
            var jsonEvent = Encoding.UTF8.GetString(@event.OriginalEvent.Data);

            var serializerSettings = new JsonSerializerSettings { ContractResolver = new PrivateSetterAndCtorContractResolver()};
            var message = JsonConvert.DeserializeObject<T>(jsonEvent, serializerSettings);

            await this.mediator.Publish(message);
            await this.StoreCheckpoint<T>(@event);

        }

        private async void OnSubscriptionDropped<T>(EventStoreCatchUpSubscription subscription, SubscriptionDropReason dropReason, Exception e)
            where T : IBusMessage
        {
            await this.Subscribe<T>();
        }

        private async Task<Maybe<CheckpointMessage>> GetCheckpointFor<T>()
            where T : IBusMessage
        {
            var stream = CheckpointFactory.GetStream<T>();
            var checkpointReadOrNothing = await storeConnection.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, 1, false, StoreCredentials).ToMaybe();
            return checkpointReadOrNothing.Where(cr => cr.Events.Any())
                .Select(cr => cr.Events[0])
                .Select(e => e.OriginalEvent.ToDecodedMessage<CheckpointMessage>());
        }

        private async Task StoreCheckpoint<T>(ResolvedEvent @event)
            where T : IBusMessage
        {
            var checkpointStream = CheckpointFactory.GetStream<T>();
            var metadata = StreamMetadata.Build().SetMaxCount(1);
            await storeConnection.SetStreamMetadataAsync(checkpointStream, ExpectedVersion.Any, metadata, StoreCredentials);

            var checkpointMessage = new CheckpointMessage(@event.OriginalEventNumber);
            var storeMessage = new EventStoreMessage(checkpointMessage);
            await storeConnection.AppendToStreamAsync(checkpointStream, ExpectedVersion.Any, storeMessage.ToEventData());
        }

        private UserCredentials StoreCredentials => new UserCredentials(this.settings.Username, this.settings.Password);
    }
}