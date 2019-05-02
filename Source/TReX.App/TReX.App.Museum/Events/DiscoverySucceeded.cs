using TReX.Kernel.Shared.Bus;

namespace TReX.App.Museum.Events
{
    public sealed class DiscoverySucceeded : IIntegrationEvent
    {
        private DiscoverySucceeded()
        {
        }

        public string DiscoveryId { get; private set; }
    }
}