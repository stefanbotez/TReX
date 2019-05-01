using EnsureThat;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Domain.Events
{
    public sealed class DiscoveryFailed : IDomainEvent
    {
        private DiscoveryFailed()
        {
        }

        public DiscoveryFailed(string discoveryId, string reason)
            : this()
        {
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);
            EnsureArg.IsNotNullOrWhiteSpace(reason);
            DiscoveryId = discoveryId;
            Reason = reason;
        }

        public string DiscoveryId { get; private set; }

        public string Reason { get; private set; }
    }
}