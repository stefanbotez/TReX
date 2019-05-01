using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TReX.App.Domain
{
    public sealed class DiscoveryStatus : ValueObject
    {
        private DiscoveryStatus(Status status)
        {
            this.Status = status;
            this.ChangedAt = DateTimeOffset.Now;
        }

        public Status Status { get; private set; }

        public DateTimeOffset ChangedAt { get; private set; }

        public static DiscoveryStatus Ongoing()
        {
            return new DiscoveryStatus(Status.Ongoing);
        }

        public static DiscoveryStatus Completed()
        {
            return new DiscoveryStatus(Status.Completed);
        }

        public static DiscoveryStatus Failed()
        {
            return new DiscoveryStatus(Status.Failed);
        }

        public static implicit operator Status(DiscoveryStatus x)
        {
            return x.Status;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Status;
            yield return this.ChangedAt;
        }
    }
}