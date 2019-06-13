namespace TReX.Discovery.Shared.Domain
{
    public sealed class Discovery
    {
        public Discovery(string id, string topic)
        {
            Id = id;
            Topic = topic;
        }

        public string Id { get; private set; }

        public string Topic { get; private set; }
    }
}