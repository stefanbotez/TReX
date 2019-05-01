using TReX.Kernel.Shared.Bus;

namespace TReX.App.Business.Discovery.Events
{
    public sealed class DiscoveryCompleted : IIntegrationEvent
    {
        private DiscoveryCompleted()
        {
        }

        public string DiscoveryId { get; private set; }
    }
}