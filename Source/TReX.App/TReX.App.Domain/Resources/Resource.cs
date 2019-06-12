using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Domain.Resources
{
    public sealed class Resource : AggregateRoot
    {
        private Resource()
        {
        }

        private Resource(ProviderDetails providerDetails, ParentDiscovery discovery, string title, string description, ResourceType type)
            : this()
        {
            ProviderDetails = providerDetails;
            Discovery = discovery;
            Title = title;
            Description = description;
            Type = type;
            PublishedAt = DateTime.Now;
        }

        public ProviderDetails ProviderDetails { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public ResourceType Type { get; private set; }

        public ParentDiscovery Discovery { get; private set; }

        private Dictionary<string, object> _Bucket { get; set; } = new Dictionary<string, object>();

        public IReadOnlyDictionary<string, object> Bucket => _Bucket;

        public DateTime PublishedAt { get; private set; }

        public static  Result<Resource> CreateMedia(ProviderDetails provider, ParentDiscovery discovery, string title, string description) 
            => Create(provider, discovery, title, description, ResourceType.Media);

        public static Result<Resource> CreateCode(ProviderDetails provider, ParentDiscovery discovery, string title, string description)
            => Create(provider, discovery, title, description, ResourceType.Code);

        public static Result<Resource> CreateDocument(ProviderDetails provider, ParentDiscovery discovery, string title, string description)
            => Create(provider, discovery, title, description, ResourceType.Documents);

        private static Result<Resource> Create(ProviderDetails provider, ParentDiscovery discovery, string title, string description, ResourceType type)
        {
            var providerResult = provider.ToMaybe().ToResult(DomainMessages.InvalidProvider);
            var discoveryResult = discovery.ToMaybe().ToResult(DomainMessages.InvalidDiscovery);
            var titleResult = title.ToMaybe().ToResult(DomainMessages.InvalidTitle).Ensure(t => !string.IsNullOrEmpty(t), DomainMessages.InvalidTitle);

            return Result.FirstFailureOrSuccess(providerResult, discoveryResult, titleResult)
                .OnSuccess(() => new Resource(provider, discovery, title, description, type));
        }

        public void FillBucket<T>(string key, T value)
            where T : class
        {
            _Bucket[key] = value;
        }

        public bool SatisfiesTopic(string topic)
        {
            if (string.IsNullOrEmpty(topic))
            {
                return true;
            }

            if (this.Discovery.Topic.Contains(topic, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return Title.Contains(topic, StringComparison.InvariantCultureIgnoreCase) || Description.Contains(topic, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}