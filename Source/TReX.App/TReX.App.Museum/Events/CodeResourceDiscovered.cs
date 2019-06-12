using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Museum.Events
{
    public sealed class CodeResourceDiscovered : IIntegrationEvent
    {
        private CodeResourceDiscovered()
        {
        }

        public string DiscoveryId { get; private set; }

        public string DiscoveryTopic { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }
    }
}