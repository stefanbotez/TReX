using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents.Session;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public sealed class RavenWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly IAsyncDocumentSession session;
        private readonly ILogger logger;
        private readonly AggregateTracker tracker;

        public RavenWriteRepository(IAsyncDocumentSession session, ILogger logger, AggregateTracker tracker)
        {
            EnsureArg.IsNotNull(session);
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(tracker);
            this.session = session;
            this.logger = logger;
            this.tracker = tracker;
        }

        public async Task<Result> CreateAsync(T aggregate)
        {
            return await Maybe<T>.From(aggregate).ToResult("Aggregate cannot be null")
                .OnSuccess(a => Extensions.TryAsync(() => this.session.StoreAsync(a, a.Id)))
                .OnFailure(e => this.logger.LogError(e))

                .OnSuccess(() => this.tracker.Track(aggregate));
        }
    }
}