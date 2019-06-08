using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace TReX.Discovery.Code.Domain
{
    public sealed class CodeResource
    {
        private CodeResource(string providerId, string title, string description)
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

        public static Result<CodeResource> Create(string providerId, string title, Maybe<string> descriptionOrNothing)
        {
            var providerIdResult = Maybe<string>.From(providerId).ToResult(DomainMessages.InvalidProviderId);
            var titleResult = Maybe<string>.From(title).ToResult(DomainMessages.InvalidTitle);
            var description = descriptionOrNothing.Unwrap(string.Empty);

            return Result.Combine(titleResult, providerIdResult)
                .OnSuccess(() => new CodeResource(providerIdResult.Value, titleResult.Value, description));
        }
    }
}
