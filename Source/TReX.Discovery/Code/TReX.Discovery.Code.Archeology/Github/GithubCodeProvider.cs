using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TReX.Discovery.Code.Archeology.Github
{
    class GithubCodeProvider
    {
        private readonly GitHubClient githubClient;

        public GithubCodeProvider()
        {
            githubClient = new GitHubClient(new ProductHeaderValue("TReX"));
        }

        public async Task<SearchRepositoryResult> Search(string query)
        {
            var request = new SearchRepositoriesRequest(query);

            return await githubClient.Search.SearchRepo(request);
        }
    }
}
