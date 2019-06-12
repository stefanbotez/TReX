using System.Collections.Generic;
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
    public sealed class FindResourcesQueryHandler : IRequestHandler<FindResourcesQuery, PaginatedResult<ResourceModel>>
    {
        private readonly IReadRepository<Resource> readRepository;

        public FindResourcesQueryHandler(IReadRepository<Resource> readRepository)
        {
            EnsureArg.IsNotNull(readRepository);
            this.readRepository = readRepository;
        }

        public async Task<PaginatedResult<ResourceModel>> Handle(FindResourcesQuery request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var resources = (await this.readRepository.GetAll())
                .Where(r => r.SatisfiesTopic(request.Topic))
                .OrderBy(request.OrderBy);

            var totalCount = resources.Count();
            var models = resources
                .TakePage(request.Page)
                .Select(r => new ResourceModel(r));

            return new PaginatedResult<ResourceModel>(models, totalCount);
        }
    }

    internal static class ResourceFilterUtils
    {
        private const string TitleAsc = "title";
        private const string TitleDesc = "-title";
        private const string Newest = "newest";
        private const int ItemsPerPage = 12;

        public static IEnumerable<Resource> OrderBy(this IEnumerable<Resource> resources, string key)
        {
            if (key == TitleAsc)
            {
                return resources.OrderBy(r => r.Title);
            }

            if (key == TitleDesc)
            {
                return resources.OrderByDescending(r => r.Title);
            }

            return resources.OrderByDescending(r => r.PublishedAt);
        }

        public static IEnumerable<Resource> TakePage(this IEnumerable<Resource> resources, int page)
        {
            return resources.Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage);
        }
    }
}