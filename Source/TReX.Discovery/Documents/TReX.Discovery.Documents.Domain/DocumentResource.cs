using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Shared.Domain;

namespace TReX.Discovery.Documents.Domain
{
    public sealed class DocumentResource
    {
        private DocumentResource(ProviderDetails details, string title, string description)
        {
            EnsureArg.IsNotNull(details);
            EnsureArg.IsNotNullOrEmpty(title);

            ProviderDetails = details;
            Title = title;
            Description = description;
        }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public static Result<DocumentResource> Create(ProviderDetails providerDetails, string title, Maybe<string> descriptionOrNothing)
        {
            var detailsResult = Maybe<ProviderDetails>.From(providerDetails).ToResult(DomainMessages.InvalidProviderDetails);
            var titleResult = Maybe<string>.From(title).ToResult(DomainMessages.InvalidTitle);
            var description = descriptionOrNothing.Unwrap(string.Empty);

            return Result.Combine(titleResult, detailsResult)
                .OnSuccess(() => new DocumentResource(providerDetails, title, description));
        }
    }
}