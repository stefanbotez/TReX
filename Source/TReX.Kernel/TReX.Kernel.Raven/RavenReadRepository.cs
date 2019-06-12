using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public sealed class RavenReadRepository<T> : IReadRepository<T>
        where T : AggregateRoot
    {
        private readonly IAsyncDocumentSession session;
        private readonly ILogger logger;
        private readonly AggregateTracker tracker;

        public RavenReadRepository(IAsyncDocumentSession session, ILogger logger, AggregateTracker tracker)
        {
            EnsureArg.IsNotNull(session);
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(tracker);
            this.session = session;
            this.logger = logger;
            this.tracker = tracker;
        }

        public async Task<Maybe<T>> GetByIdAsync(string id)
        {
            return await Result.Try(() => this.session.LoadAsync<T>(id))
                .OnSuccess(a => this.tracker.Track(a))
                .OnFailure(e => this.logger.LogError(e))
                .ToMaybe();
        }

        public async Task<Result<IEnumerable<T>>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await Result.Try<IEnumerable<T>>(async () => await this.session.Query<T>().Where(t => t.Id.In(ids)).ToListAsync())
                .OnSuccess(list =>
                {
                    foreach (var aggregate in list)
                    {
                        this.tracker.Track(aggregate);
                    }
                })
                .OnFailure(e => this.logger.LogError(e));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.session.Query<T>().ToListAsync();
        }
    }
}