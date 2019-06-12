using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public sealed class ProviderDetails : ValueObject
    {
        private ProviderDetails()
        {
        }

        public string ExternalId { get; private set; }

        public string Name { get; private set; }

        public static Result<ProviderDetails> Create(string externalId, string name)
        {
            var idResult = Maybe<string>.From(externalId).ToResult(KernelSharedMessages.InvalidExternalId)
                .Ensure(i => !string.IsNullOrEmpty(i), KernelSharedMessages.InvalidExternalId);
            var nameResult = Maybe<string>.From(name).ToResult(KernelSharedMessages.InvalidProviderName)
                .Ensure(i => !string.IsNullOrEmpty(i), KernelSharedMessages.InvalidProviderName);

            return Result.FirstFailureOrSuccess(idResult, nameResult)
                .OnSuccess(() => new ProviderDetails
                {
                    ExternalId = externalId,
                    Name = name
                });
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExternalId;
            yield return Name;
        }
    }
}