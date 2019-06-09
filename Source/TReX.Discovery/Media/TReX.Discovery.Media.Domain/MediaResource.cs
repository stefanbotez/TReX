using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Shared.Domain;

namespace TReX.Discovery.Media.Domain 
{
    public sealed class MediaResource
    {
        private MediaResource(ProviderDetails providerDetails, string title, string description)
        {
            EnsureArg.IsNotNullOrWhiteSpace(title);
            EnsureArg.IsNotNull(providerDetails);
            this.Title = title;
            this.ProviderDetails = providerDetails;
            this.Description = description;
        }

        public string Title { get; private set; }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Description { get; private set; }

        public Thumbnail Thumbnail { get; private set; }

        public static Result<MediaResource> Create(ProviderDetails providerDetails, string title, Maybe<string> descriptionOrNothing)
        {
            var detailsResult = Maybe<ProviderDetails>.From(providerDetails).ToResult(DomainMessages.InvalidProviderDetails);
            var titleResult = Maybe<string>.From(title).ToResult(DomainMessages.InvalidTitle);
            var description = descriptionOrNothing.Unwrap(string.Empty);

            return Result.Combine(titleResult, detailsResult)
                .OnSuccess(() => new MediaResource(providerDetails, title, description));
        }

        public static Result<MediaResource> Create(ProviderDetails providerDetails, string title, Maybe<string> descriptionOrNothing, Maybe<Thumbnail> thumbnailOrNothing)
        {
            return Create(providerDetails, title, descriptionOrNothing)
                .OnSuccess(mr => { mr.Thumbnail = thumbnailOrNothing.Unwrap(); });
        }
    }
}