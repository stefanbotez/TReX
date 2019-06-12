using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Business.Resources.Models;
using TReX.App.Business.Resources.Queries;
using TReX.App.Domain.Resources;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Resources.QueryHandlers
{
    public sealed class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, Maybe<ResourceModel>>
    {
        private readonly IReadRepository<Resource> readRepository;

        public GetResourceQueryHandler(IReadRepository<Resource> readRepository)
        {
            EnsureArg.IsNotNull(readRepository);
            this.readRepository = readRepository;
        }

        public async Task<Maybe<ResourceModel>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var resourceOrNothing = await readRepository.GetByIdAsync(request.Id);

            return resourceOrNothing.Select(r => new ResourceModel(r));
        }
    }
}