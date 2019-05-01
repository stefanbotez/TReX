using EnsureThat;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Domain.Events
{
    public sealed class DiscoveryStatusChanged : IDomainEvent
    {
        public DiscoveryStatusChanged(string discoveryId, Status status)
        {
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);

            DiscoveryId = discoveryId;
            Status = status.ToString();
        }

        public string DiscoveryId { get; private set; }

        public string Status { get; private set; }
    }
}