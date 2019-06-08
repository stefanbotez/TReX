using Octokit;
using System;
using TReX.Discovery.Code.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    internal class GithubCodeLecture : ICodeLecture
    {
        private GithubCodeLecture()
        {
        }

        public GithubCodeLecture(Repository result) : this()
        {
            RepositoryId = result.Id.ToString();
            Title = result.Name;
            Description = result.Description;
            PublishedAt = result.PushedAt.Value.DateTime;
        }

        public string RepositoryId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public CodeResource ToResource()
        {
            throw new NotImplementedException();
        }
    }
}
