using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public sealed class RavenReadRepository<T> : IReadRepository<T>
        where T : AggregateRoot
    {
        private readonly IAsyncDocumentSession session;

        public RavenReadRepository(IAsyncDocumentSession session)
        {
            EnsureArg.IsNotNull(session);
            this.session = session;
        }

        public async Task<Maybe<T>> GetByIdAsync(string id)
        {
            return await this.session.LoadAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await this.session.Query<T>().Where(t => t.Id.In(ids)).ToListAsync();
        }
    }
}