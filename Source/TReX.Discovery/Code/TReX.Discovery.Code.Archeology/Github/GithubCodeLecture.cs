using Octokit;
using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Text;
using TReX.Discovery.Code.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    class GithubCodeLecture : AggregateRoot, ICodeLecture
=======
using TReX.Discovery.Code.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    internal class GithubCodeLecture : ICodeLecture
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df
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

<<<<<<< HEAD
        public CodeResource ToCodeLecture()
        {
            return CodeResource.Create(CodeId, Title, Description).Value;
=======
        public CodeResource ToResource()
        {
            throw new NotImplementedException();
>>>>>>> 5f3ed5b0972aae99e89a4fee1f8c388cad4359df
        }
    }
}
