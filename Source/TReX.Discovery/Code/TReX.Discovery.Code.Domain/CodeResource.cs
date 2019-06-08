using System;
using TReX.Discovery.Shared.Domain;

namespace TReX.Discovery.Code.Domain
{
    public sealed class CodeResource
    {
        private CodeResource()
        {
        }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }
    }
}