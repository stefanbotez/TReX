using EnsureThat;

namespace TReX.Kernel.Raven
{
    public sealed class RavenSettings
    {
        public RavenSettings(string databaseName, string serverUrl)
        {
            EnsureArg.IsNotNullOrWhiteSpace(databaseName);
            EnsureArg.IsNotNullOrWhiteSpace(serverUrl);
            DatabaseName = databaseName;
            ServerUrl = serverUrl;
        }

        public string DatabaseName { get; private set; }

        public string ServerUrl { get; private set; }
    }
}