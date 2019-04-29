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

        public EventStoreBus(IEventStoreConnection storeConnection)
        {
            EnsureArg.IsNotNull(storeConnection);
            this.storeConnection = storeConnection;
        }

        public async Task<Result> PublishMessages<T>(IEnumerable<T> messages) 
            where T : IBusMessage
        {
            var messagesByType = messages.ToLookup(m => m.GetType());
            var tasks = messagesByType.Select(group =>
            {
                var topic = GetTopicName(group.Key);
                var events = group.Select(e => new EventStoreMessage(e))
                    .Select(e => e.ToEventData());

                return Extensions.TryAsync(() => this.storeConnection.AppendToStreamAsync(topic, ExpectedVersion.Any, events));
            });

            return Result.Combine(await Task.WhenAll(tasks));
        }

        private static string GetTopicName(Type messageType)
        {
            return $"{messageType.Name}-Topic";
        }
    }
}