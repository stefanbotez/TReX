using TReX.Kernel.Shared.Bus;

namespace TReX.Discovery.Media.Worker.Events
{
    public sealed class DiscoveryCreated : IBusMessage
    {
        private DiscoveryCreated()
        {
        }

        public DiscoveryCreated(string discoveryId, string topic)
            : this()
        {
            DiscoveryId = discoveryId;
            Topic = topic;
        }

        public string DiscoveryId { get; private set; }

        public string Topic { get; private set; }
    }
}