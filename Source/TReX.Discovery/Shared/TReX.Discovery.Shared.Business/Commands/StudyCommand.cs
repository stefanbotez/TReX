using EnsureThat;

namespace TReX.Discovery.Shared.Business.Commands
{
    public sealed class StudyCommand
    {
        public StudyCommand(string topic, string discoveryId)
        {
            EnsureArg.IsNotNullOrWhiteSpace(topic);
            EnsureArg.IsNotNullOrWhiteSpace(discoveryId);
            Topic = topic;
            DiscoveryId = discoveryId;
        }

        public string Topic { get; }

        public string DiscoveryId { get; }
    }
}