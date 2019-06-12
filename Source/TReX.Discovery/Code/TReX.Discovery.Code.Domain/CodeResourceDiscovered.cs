using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Code.Domain
{
    public sealed class CodeResourceDiscovered : IDomainEvent
    {
        public CodeResourceDiscovered(string discoveryId, CodeResource resource)
        {
            DiscoveryId = discoveryId;

            Title = resource.Title;
            ProviderDetails = resource.ProviderDetails;
            Description = resource.Description;
        }

        public string DiscoveryId { get; private set; }

        public string Title { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Description { get; private set; }
    }
}
