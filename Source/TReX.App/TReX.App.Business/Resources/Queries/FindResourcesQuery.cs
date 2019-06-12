using MediatR;
using TReX.App.Business.Resources.Models;

namespace TReX.App.Business.Resources.Queries
{
    public sealed class FindResourcesQuery : IRequest<PaginatedResult<ResourceModel>>
    {
        public FindResourcesQuery(string topic, int page, string orderBy)
        {
            Topic = topic;
            Page = page;
            OrderBy = orderBy;
        }

        public string Topic { get; }

        public int Page { get; }

        public string OrderBy { get; set; }
    }
}