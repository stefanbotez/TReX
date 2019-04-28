using EnsureThat;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeSettings
    {
        public YoutubeSettings(string apiKey, string appName, string requestPart, string resourceType, int maxResults)
        {
            EnsureArg.IsNotNullOrWhiteSpace(apiKey);
            EnsureArg.IsNotNullOrWhiteSpace(appName);
            EnsureArg.IsNotNullOrWhiteSpace(requestPart);
            EnsureArg.IsNotNullOrWhiteSpace(resourceType);

            ApiKey = apiKey;
            AppName = appName;
            RequestPart = requestPart;
            ResourceType = resourceType;
            MaxResults = maxResults;
        }

        public string ApiKey { get; }

        public string AppName { get; }

        public string RequestPart { get; }

        public string ResourceType { get; }

        public int MaxResults { get; }
    }
}