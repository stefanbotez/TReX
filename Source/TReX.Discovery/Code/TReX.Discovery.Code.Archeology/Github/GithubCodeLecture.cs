using Octokit;
using System;
using CSharpFunctionalExtensions;
using TReX.Discovery.Code.Domain;
using TReX.Discovery.Shared.Archeology;
using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    public class GithubCodeLecture : AggregateRoot, ILecture<CodeResource>
    {
        private GithubCodeLecture()
        {
        }

        public GithubCodeLecture(Repository result) : this()
        {
            RepositoryId = result.Id.ToString();
            Id = RepositoryId;
            Title = result.Name;
            Description = result.Description;
            PublishedAt = result.PushedAt.Value.DateTime;
        }

        public string RepositoryId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public Result<CodeResource> ToResource()
        {
            return ProviderDetails.Create(RepositoryId, Constants.Github)
                .OnSuccess(pd => CodeResource.Create(pd, Title, Description));
        }
    }
}
