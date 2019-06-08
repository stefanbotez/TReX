using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TReX.Discovery.Shared.Domain
{
    public sealed class ProviderDetails : ValueObject
    {
        private ProviderDetails()
        {
        }

        public static Result<ProviderDetails> Create(string externalId, string name)
        {
            var idResult = Maybe<string>.From(externalId).ToResult(SharedDomainMessages.InvalidExternalId)
                .Ensure(i => !string.IsNullOrEmpty(i), SharedDomainMessages.InvalidExternalId);
            var nameResult = Maybe<string>.From(name).ToResult(SharedDomainMessages.InvalidProviderName)
                .Ensure(i => !string.IsNullOrEmpty(i), SharedDomainMessages.InvalidProviderName);

            return Result.FirstFailureOrSuccess(idResult, nameResult)
                .OnSuccess(() => new ProviderDetails
                {
                    ExternalId = externalId,
                    Name = name
                });
        }

        public string ExternalId { get; private set; }

        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExternalId;
            yield return Name;
        }
    }
}