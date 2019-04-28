using TReX.Discovery.Kernel.Shared;

namespace TReX.Discovery.Media.Domain
{
    public sealed class MediaResourceDiscovered : IDomainEvent
    {
        public MediaResourceDiscovered(MediaResource resource)
        {
            Title = resource.Title;
            ProviderId = resource.ProviderId;
            Description = resource.Description;
            ThumbnailUrl = resource.Thumbnail.Url;
        }

        public string Title { get; private set; }

        public string ProviderId { get; private set; }

        public string Description { get; private set; }

        public string ThumbnailUrl { get; private set; }
    }
}