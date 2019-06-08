using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TReX.Discovery.Code.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    class GithubCodeLecture : AggregateRoot, ICodeLecture
    {
        private GithubCodeLecture()
        {
        }

        public GithubCodeLecture(Repository result) : this()
        {
            CodeId = result.Id.ToString();
            Title = result.Name;
            Description = result.Description;
            PublishedAt = result.PushedAt.Value.DateTime;
        }

        public string CodeId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public CodeResource ToCodeLecture()
        {
            return CodeResource.Create(CodeId, Title, Description).Value;
        }
    }
}
