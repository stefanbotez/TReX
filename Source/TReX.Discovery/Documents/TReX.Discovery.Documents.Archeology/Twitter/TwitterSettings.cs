using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;

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

        public string ConsumerKey { get; } //AlffsFObuC02EcAdmiNexiJOl 

        public string ConsumerSecret { get; } //tepIgaeUE0wVFSuHa9z2aUvCCKTMD9tMt5ZxxyZBGWyUakhVUo 

        public string ApiKey { get; } //2388862608-ZB71dptULrjtTZSHZDpZ8KEXdvcmzNRConjp3RP

        public string ApiSecret { get; } //OBI8v2BQGRN3HWWKLTK1slKUxtntLjzqTz3Mj9beRx7rY 

        public int PerPage { get; }

        public int MaxDepth { get; }
    }
}
