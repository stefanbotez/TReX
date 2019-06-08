using System;
using CSharpFunctionalExtensions;
using Google.Apis.YouTube.v3.Data;
using TReX.Discovery.Media.Domain;
using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;
using Thumbnail = Google.Apis.YouTube.v3.Data.Thumbnail;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeMediaLecture : AggregateRoot, IMediaLecture
    {
        private YoutubeMediaLecture()
        {
        }

        public YoutubeMediaLecture(SearchResult result) : this()
        {
            VideoId = result.Id.VideoId;
            Id = VideoId;

            Title = result.Snippet.Title;
            Description = result.Snippet.Description;
            Thumbnail = result.Snippet.Thumbnails.High;
            PublishedAt = result.Snippet.PublishedAt.GetValueOrDefault(DateTime.Now);
        }

        public string VideoId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public Thumbnail Thumbnail { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public Result<MediaResource> ToResource()
        {
            var providerDetailsResult = ProviderDetails.Create(VideoId, Constants.Youtube);
            var thumbnail = Thumbnail.ToMaybe().Unwrap(t => Domain.Thumbnail.Create(t.Url).Value);

            return providerDetailsResult.OnSuccess(pd => MediaResource.Create(pd, Title, Description, thumbnail));
        }
    }
}