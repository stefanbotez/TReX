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

        public RavenWriteRepository(IAsyncDocumentSession session)
        {
            EnsureArg.IsNotNull(session);
            this.session = session;
        }

        public async Task<Result> CreateAsync(T aggregate)
        {
            return await Maybe<T>.From(aggregate).ToResult("Aggregate cannot be null")
                .OnSuccess(a => Extensions.TryAsync(() => this.session.StoreAsync(a, a.Id)));
        }
    }
}