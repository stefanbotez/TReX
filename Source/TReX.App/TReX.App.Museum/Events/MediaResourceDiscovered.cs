using TReX.Kernel.Shared.Bus;

namespace TReX.App.Museum.Events
{
    public sealed class MediaResourceDiscovered : IIntegrationEvent
    {
        private MediaResourceDiscovered()
        {
        }

        public string DiscoveryId { get; private set; }

        public string Title { get; private set; }

        public string ProviderId { get; private set; }

        public string Description { get; private set; }

        public string ThumbnailUrl { get; private set; }
    }
}