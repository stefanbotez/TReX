using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Domain.Events
{
    public sealed class DocumentResourceDiscovered : IDomainEvent
    {
        public DocumentResourceDiscovered(string discoveryId, DocumentResource resource)
        {
            DiscoveryId = discoveryId;
            ProviderDetails = resource.ProviderDetails;
            Title = resource.Title;
            Description = resource.Description;
        }

        public string DiscoveryId { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}