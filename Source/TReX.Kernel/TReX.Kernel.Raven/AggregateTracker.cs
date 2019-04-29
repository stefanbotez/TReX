using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public sealed class AggregateTracker
    {
        private readonly ICollection<AggregateRoot> trackedAggregates = new List<AggregateRoot>();

        public void Track(AggregateRoot aggregate)
        {
            this.trackedAggregates.FirstOrNothing(a => a.Id == aggregate.Id).ToResult("Aggregate not already tracked")
                .OnFailureCompensate(() => this.AddAggregate(aggregate));
        }

        public IEnumerable<AggregateRoot> TrackedAggregates => this.trackedAggregates;

        public IEnumerable<IDomainEvent> DumpEvents()
        {
            var events = new List<IDomainEvent>();
            foreach (var aggregate in trackedAggregates)
            {
                events.AddRange(aggregate.Events.ToList());
                aggregate.ClearEvents();
            }

            return events;
        }

        private Result AddAggregate(AggregateRoot aggregate)
        {
            return Maybe<AggregateRoot>.From(aggregate).ToResult("Invalid aggregate")
                .OnSuccess(a => this.trackedAggregates.Add(a));
        }
    }
}