using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Business.Resources.Models;

namespace TReX.App.Business.Resources.Queries
{
    public sealed class GetResourceQuery : IRequest<Maybe<ResourceModel>>
    {
        public GetResourceQuery(string id)
        {
            EnsureArg.IsNotNullOrEmpty(id);
            Id = id;
        }

        public string Id { get; }
    }
}