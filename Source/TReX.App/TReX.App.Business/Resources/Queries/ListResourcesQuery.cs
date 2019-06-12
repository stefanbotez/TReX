using MediatR;
using TReX.App.Business.Resources.Models;

namespace TReX.App.Business.Resources.Queries
{
    public sealed class ListResourcesQuery : IRequest<PaginatedResult<ResourceModel>>
    {
    }
}