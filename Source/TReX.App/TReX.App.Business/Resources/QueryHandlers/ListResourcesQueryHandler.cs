using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using TReX.App.Business.Resources.Models;
using TReX.App.Business.Resources.Queries;
using TReX.App.Domain.Resources;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Resources.QueryHandlers
{
    public sealed class ListResourcesQueryHandler : IRequestHandler<ListResourcesQuery, PaginatedResult<ResourceModel>>
    {
        private readonly IReadRepository<Resource> readRepository;

        public ListResourcesQueryHandler(IReadRepository<Resource> readRepository)
        {
            EnsureArg.IsNotNull(readRepository);
            this.readRepository = readRepository;
        }

        public async Task<PaginatedResult<ResourceModel>> Handle(ListResourcesQuery request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var models = (await this.readRepository.GetAll())
                .Select(r => new ResourceModel(r));

            return new PaginatedResult<ResourceModel>(models, models.Count());
        }
    }
}