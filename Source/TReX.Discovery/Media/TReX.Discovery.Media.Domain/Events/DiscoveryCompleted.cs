using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Domain.Events
{
    public sealed class DiscoveryCompleted : IDomainEvent
    {
        private DiscoveryCompleted()
        {
        }

        public DiscoveryCompleted(string discoveryId) 
            : this()
        {
            DiscoveryId = discoveryId;
        }

        public string DiscoveryId { get; private set; }
    }
}