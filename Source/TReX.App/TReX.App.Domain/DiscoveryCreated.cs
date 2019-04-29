using EnsureThat;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Domain
{
    public sealed class DiscoveryCreated : IDomainEvent
    {
        private DiscoveryCreated()
        {
        }

        public DiscoveryCreated(string discoveryId, string topic) 
            : this()
        {
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);
            EnsureArg.IsNotNullOrWhiteSpace(topic);

            DiscoveryId = discoveryId;
            Topic = topic;
        }

        public string DiscoveryId { get; private set; }

        public string Topic { get; private set; }
    }
}