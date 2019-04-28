using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace TReX.Discovery.Media.Domain
{
    public sealed class Thumbnail : ValueObject
    {
        private Thumbnail(string url)
        {
            EnsureArg.IsNotNullOrWhiteSpace(url);
            this.Url = url;
        }

        public static Result<Thumbnail> Create(string url)
        {
            return Maybe<string>.From(url).ToResult(DomainMessages.InvalidUrl)
                .OnSuccess(u => new Thumbnail(u));
        }

        public string Url { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Url;
        }
    }
}