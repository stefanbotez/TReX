using EnsureThat;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeSettings
    {
        public YoutubeSettings(string apiKey, string appName, string requestPart, string resourceType, int maxResults, int maxDepth)
        {
            EnsureArg.IsNotNullOrWhiteSpace(apiKey);
            EnsureArg.IsNotNullOrWhiteSpace(appName);
            EnsureArg.IsNotNullOrWhiteSpace(requestPart);
            EnsureArg.IsNotNullOrWhiteSpace(resourceType);
            EnsureArg.IsGte(maxDepth, 1);

            ApiKey = apiKey;
            AppName = appName;
            RequestPart = requestPart;
            ResourceType = resourceType;
            MaxResults = maxResults;
            MaxDepth = maxDepth;
        }

        public string ApiKey { get; }

        public string AppName { get; }

        public string RequestPart { get; }

        public string ResourceType { get; }

        public int MaxResults { get; }

        public int MaxDepth { get; }
    }
}