using System;
using CSharpFunctionalExtensions;
using TReX.Discovery.Media.Domain;
using TReX.Discovery.Shared.Archeology;
using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Archeology.Vimeo
{
    public sealed class VimeoMediaLecture : AggregateRoot, ILecture<MediaResource>
    {
        private VimeoMediaLecture()
        {
        }

        public VimeoMediaLecture(string videoId, string title, string description, Thumbnail thumbnail, DateTime publishedAt)
        {
            VideoId = videoId;
            Title = title;
            Description = description;
            Thumbnail = thumbnail;
            PublishedAt = publishedAt;
        }

        public string VideoId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public Thumbnail Thumbnail { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public Result<MediaResource> ToResource()
        {
            return ProviderDetails.Create(VideoId, Constants.Vimeo)
                .OnSuccess(pd => MediaResource.Create(pd, Title, Description, Thumbnail));
        }
    }
}

