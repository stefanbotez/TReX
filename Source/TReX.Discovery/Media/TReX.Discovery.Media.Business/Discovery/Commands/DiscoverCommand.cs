using EnsureThat;

namespace TReX.Discovery.Media.Business.Discovery.Commands
{
    public class DiscoverCommand
    {
        public DiscoverCommand(string topic, string discoveryId)
        {
            EnsureArg.IsNotEmptyOrWhitespace(topic);
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);
            Topic = topic;
            DiscoveryId = discoveryId;
        }

        public string Topic { get; }

        public string DiscoveryId { get; }
    }
}