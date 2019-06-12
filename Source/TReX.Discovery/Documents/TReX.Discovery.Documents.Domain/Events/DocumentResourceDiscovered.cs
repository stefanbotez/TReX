using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Domain.Events
{
    public sealed class DocumentResourceDiscovered : IDomainEvent
    {
        public DocumentResourceDiscovered(Shared.Domain.Discovery discovery, DocumentResource resource)
        {
            DiscoveryId = discovery.Id;
            DiscoveryTopic = discovery.Topic;

            ProviderDetails = resource.ProviderDetails;
            Title = resource.Title;
            Description = resource.Description;
        }

        public string DiscoveryId { get; private set; }

        public string DiscoveryTopic { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}