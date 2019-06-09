using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Domain
{
    public sealed class MediaResourceDiscovered : IDomainEvent
    {
        public MediaResourceDiscovered(string discoveryId, MediaResource resource)
        {
            DiscoveryId = discoveryId;

            Title = resource.Title;
            ProviderDetails = resource.ProviderDetails;
            Description = resource.Description;
            ThumbnailUrl = resource.Thumbnail.Url;
        }

        public string DiscoveryId { get; private set; }

        public string Title { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Description { get; private set; }

        public string ThumbnailUrl { get; private set; }
    }
}