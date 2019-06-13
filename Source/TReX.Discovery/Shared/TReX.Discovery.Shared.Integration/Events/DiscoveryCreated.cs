using TReX.Kernel.Shared.Bus;

namespace TReX.Discovery.Shared.Integration.Events
{
    public sealed class DiscoveryCreated : IIntegrationEvent
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