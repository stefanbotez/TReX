using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public sealed class EventStoreMessage
    {
        public EventStoreMessage(IBusMessage message)
        {
            this.Data = message;
            this.EventType = message.GetType().Name;
            this.Metadata = new MessageMetadata(message.GetType());
        }

        private string EventType { get; set; }

        public object Data { get; private set; }

        public MessageMetadata Metadata { get; private set; }

        public EventData ToEventData()
        {
            var serializedData = JsonConvert.SerializeObject(this.Data);
            var serializedMetadata = JsonConvert.SerializeObject(this.Metadata);

            return new EventData(Guid.NewGuid(), EventType, true, Encoding.UTF8.GetBytes(serializedData), Encoding.UTF8.GetBytes(serializedMetadata));
        }
    }

    public sealed class MessageMetadata
    {
        public MessageMetadata(Type messageType)
        {
            ClrType = $"{messageType.FullName}, {messageType.Assembly.GetName().Name}";
        }

        public string ClrType { get; private set; }
    }
}