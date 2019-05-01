using TReX.Kernel.Shared.Bus;

namespace TReX.App.Business.Discovery.Events
{
    public sealed class DiscoveryFailed : IIntegrationEvent
    {
        private DiscoveryFailed()
        {
        }

        public string DiscoveryId { get; private set; }

        public string Reason { get; private set; }
    }
}