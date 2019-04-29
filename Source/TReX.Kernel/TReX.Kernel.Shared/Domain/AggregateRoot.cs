using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public abstract class AggregateRoot : Entity
    {
        private readonly ICollection<IDomainEvent> events = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => this.events;

        public void AddDomainEvent(IDomainEvent @event)
        {
            Maybe<IDomainEvent>.From(@event).ToResult("Invalid event")
                .OnSuccess(e => this.events.Add(e));
        }

        public void ClearEvents()
        {
            this.events.Clear();
        }
    }
}