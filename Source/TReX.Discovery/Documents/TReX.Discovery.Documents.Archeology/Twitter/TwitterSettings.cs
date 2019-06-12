using EnsureThat;

namespace TReX.Discovery.Documents.Archeology.Twitter
{
    public class TwitterSettings
    {
        public TwitterSettings(string consumerKey, string consumerSecret, string apiKey, string apiSecret, int perPage, int maxDepth)
        {
            EnsureArg.IsNotNullOrWhiteSpace(consumerKey);
            EnsureArg.IsNotNullOrWhiteSpace(consumerSecret);
            EnsureArg.IsNotNullOrWhiteSpace(apiKey);
            EnsureArg.IsNotNullOrWhiteSpace(apiSecret);

            EnsureArg.IsGte(maxDepth, 1);

            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            PerPage = perPage;
            MaxDepth = maxDepth;
        }

        public string ConsumerKey { get; }

        public string ConsumerSecret { get; } 

        public string ApiKey { get; } 

        public string ApiSecret { get; }

        public int PerPage { get; }

        public int MaxDepth { get; }
    }
}
