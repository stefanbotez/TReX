using System.Collections.Generic;
using System.Linq;
using TReX.App.Domain.Resources;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Resources.Models
{
    public sealed class ResourceModel
    {
        public ResourceModel(Resource resource)
        {
            Id = resource.Id;
            Title = resource.Title;
            Description = resource.Description;
            Type = resource.Type;
            Provider = resource.ProviderDetails;
            Discovery = resource.Discovery;

            var camelCasePairs = resource.Bucket.Select(i => new KeyValuePair<string, object>(i.Key.PascalToCamelCase(), i.Value));
            Bucket = new Dictionary<string, object>(camelCasePairs);
        }

        public string Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public ResourceType Type { get; private set; }

        public ProviderDetails Provider { get; private set; }

        public ParentDiscovery Discovery { get; private set; }

        public IReadOnlyDictionary<string, object> Bucket { get; private set; }
    }
}