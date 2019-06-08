using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EnsureThat;
=======
using TReX.Discovery.Shared.Domain;
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df

namespace TReX.Discovery.Code.Domain
{
    public sealed class CodeResource
    {
<<<<<<< HEAD
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
=======
        private CodeResource()
        {
        }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }
    }
}
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df
