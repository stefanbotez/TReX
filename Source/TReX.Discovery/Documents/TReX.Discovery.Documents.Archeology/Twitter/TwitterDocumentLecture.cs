using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using TReX.Discovery.Documents.Domain;
using TReX.Discovery.Shared.Archeology;
using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Archeology.Twitter
{
    public class TwitterDocumentLecture : AggregateRoot, ILecture<DocumentResource>
    {
        private TwitterDocumentLecture()
        {
        }

        public TwitterDocumentLecture(string tweetId, string author, string description, DateTime publishedAt)
        {
            TweetId = tweetId;
            Author = author;
            Description = description;
            PublishedAt = publishedAt;
        }

        public string TweetId { get; private set; }

        public string Author { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public Result<DocumentResource> ToResource()
        {
            return ProviderDetails.Create(TweetId, Constants.Twitter)
                .OnSuccess(pd => DocumentResource.Create(pd, Author, Description));
        }
    }
}
