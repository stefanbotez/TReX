using System;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Domain
{
    public sealed class Discovery : AggregateRoot
    {
        private Discovery()
        {
        }

        private Discovery(Behalf behalf, string topic) 
            : this()
        {
            EnsureArg.IsNotNull(behalf);
            EnsureArg.IsNotNullOrWhiteSpace(topic);
            this.Behalf = behalf;
            this.Topic = topic;
            this.CreatedAt = DateTimeOffset.Now;

            this.AddDomainEvent(new DiscoveryCreated(this.Id, topic));
        }

        public Behalf Behalf { get; private set; }

        public string Topic { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public static Result<Discovery> CreateOnBehalfOf(Behalf behalf, string topic)
        {
            var behalfResult = Maybe<Behalf>.From(behalf).ToResult(DomainMessages.InvalidBehalf);
            var topicResult = Maybe<string>.From(topic).ToResult(DomainMessages.InvalidTopic)
                .Ensure(t => !string.IsNullOrWhiteSpace(t), DomainMessages.InvalidTopic);

            return Result.FirstFailureOrSuccess(behalfResult, topicResult)
                .OnSuccess(() => new Discovery(behalf, topic));
        }
    } 
}