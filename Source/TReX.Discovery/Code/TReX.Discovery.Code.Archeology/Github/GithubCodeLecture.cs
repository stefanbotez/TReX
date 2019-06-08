using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReX.Discovery.Code.Archeology.Github
{
    class GithubCodeLecture
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
    }
}
