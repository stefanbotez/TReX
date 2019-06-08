using CSharpFunctionalExtensions;
using EnsureThat;

namespace TReX.Discovery.Media.Domain 
{
    public sealed class MediaResource
    {
        private MediaResource(string providerId, string title, string description)
        {
            EnsureArg.IsNotNullOrWhiteSpace(title);
            EnsureArg.IsNotNullOrWhiteSpace(providerId);
            this.Title = title;
            this.ProviderId = providerId;
            this.Description = description;
        }

        public string Title { get; private set; }

        public string ProviderId { get; private set; }

        public string Description { get; private set; }

        public Thumbnail Thumbnail { get; private set; }

        public static Result<MediaResource> Create(string providerId, string title, Maybe<string> descriptionOrNothing)
        {
            var providerIdResult = Maybe<string>.From(providerId).ToResult(DomainMessages.InvalidProviderId);
            var titleResult = Maybe<string>.From(title).ToResult(DomainMessages.InvalidTitle);
            var description = descriptionOrNothing.Unwrap(string.Empty);

            return Result.Combine(titleResult, providerIdResult)
                .OnSuccess(() => new MediaResource(providerIdResult.Value, titleResult.Value, description));
        }

        public static Result<MediaResource> Create(string providerId, string title, Maybe<string> descriptionOrNothing, Maybe<Thumbnail> thumbnailOrNothing)
        {
            return Create(providerId, title, descriptionOrNothing)
                .OnSuccess(mr => { mr.Thumbnail = thumbnailOrNothing.Unwrap(); });
        }
    }
}