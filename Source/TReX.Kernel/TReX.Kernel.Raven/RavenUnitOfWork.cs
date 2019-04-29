using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents.Session;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public sealed class RavenUnitOfWork : IUnitOfWork
    {
        private readonly IAsyncDocumentSession session;

        public RavenUnitOfWork(IAsyncDocumentSession session)
        {
            EnsureArg.IsNotNull(session);
            this.session = session;
        }

        public async Task<Result> CommitAsync()
        {
            return await Extensions.TryAsync(() => this.session.SaveChangesAsync());
        }
    }
}