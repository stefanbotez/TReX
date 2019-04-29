using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using EventStore.ClientAPI;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public sealed class EventStoreBus : IMessageBus
    {
        private readonly IEventStoreConnection storeConnection;
        private readonly EventStoreSubscriptionFactory subscriptionFactory;

        private readonly Dictionary<Type, EventStoreCatchUpSubscription> subscriptions = new Dictionary<Type, EventStoreCatchUpSubscription>();

        public EventStoreBus(IEventStoreConnection storeConnection, EventStoreSubscriptionFactory subscriptionFactory)
        {
            EnsureArg.IsNotNull(storeConnection);
            EnsureArg.IsNotNull(subscriptionFactory);
            this.storeConnection = storeConnection;
            this.subscriptionFactory = subscriptionFactory;
        }

        public async Task<Result> PublishMessages<T>(IEnumerable<T> messages) 
            where T : IBusMessage
        {
            var messagesByTopic = messages.ToLookup(m => TopicFactory.GetTopic(m));
            var tasks = messagesByTopic.Select(group =>
            {
                var events = group.Select(e => new EventStoreMessage(e))
                    .Select(e => e.ToEventData());

                return Extensions.TryAsync(() => this.storeConnection.AppendToStreamAsync(group.Key, ExpectedVersion.Any, events));
            });

            return Result.Combine(await Task.WhenAll(tasks));
        }

        public async Task SubscribeTo<T>() 
            where T : IBusMessage
        {
            if (subscriptions.ContainsKey(typeof(T)))
            {
                return;
            }

            var subscription = await this.subscriptionFactory.CreateSubscription<T>();
            subscriptions[typeof(T)] = subscription;
        }
    }
}