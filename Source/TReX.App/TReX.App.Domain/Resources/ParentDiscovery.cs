using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TReX.Kernel.Shared;

namespace TReX.App.Domain.Resources
{
    public sealed class ParentDiscovery : ValueObject
    {
        private ParentDiscovery()
        {
        }

        public string Id { get; private set; }

        public string Topic { get; private set; }

        public static Result<ParentDiscovery> Create(string id, string topic)
        {
            var idResult = id.ToMaybe().ToResult(DomainMessages.InvalidDiscovery);
            var topicResult = topic.ToMaybe().ToResult(DomainMessages.InvalidTopic);

            return Result.FirstFailureOrSuccess(idResult, topicResult)
                .OnSuccess(() => new ParentDiscovery
                {
                    Id = id,
                    Topic = topic
                });
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Topic;
        }
    }
}