using EnsureThat;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Domain.Events
{
    public sealed class DiscoverySucceeded : IDomainEvent
    {
        private DiscoverySucceeded()
        {
        }

        public DiscoverySucceeded(string discoveryId) 
            : this()
        {
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);
            DiscoveryId = discoveryId;
        }

        public string DiscoveryId { get; private set; }
    }
}