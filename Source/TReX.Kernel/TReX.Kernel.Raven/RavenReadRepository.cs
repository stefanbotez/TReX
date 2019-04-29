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
        private readonly AggregateTracker tracker;

        public RavenReadRepository(IAsyncDocumentSession session, AggregateTracker tracker)
        {
            EnsureArg.IsNotNull(session);
            EnsureArg.IsNotNull(tracker);
            this.session = session;
            this.tracker = tracker;
        }

        public async Task<Maybe<T>> GetByIdAsync(string id)
        {
            return await Extensions.TryAsync(() => this.session.LoadAsync<T>(id))
                .OnSuccess(a => this.tracker.Track(a))
                .ToMaybe();
        }

        public async Task<Result<IEnumerable<T>>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await Extensions.TryAsync<IEnumerable<T>>(async () => await this.session.Query<T>().Where(t => t.Id.In(ids)).ToListAsync())
                .OnSuccess(list =>
                {
                    foreach (var aggregate in list)
                    {
                        this.tracker.Track(aggregate);
                    }
                });
        }
    }
}