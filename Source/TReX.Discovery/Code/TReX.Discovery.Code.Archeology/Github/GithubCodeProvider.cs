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

        public List<GithubCodeLecture> ToGithubCodeLecture(SearchRepositoryResult result, int page, int per_page)
        {
            List<GithubCodeLecture> resultList = new List<GithubCodeLecture>();

            for (int i = (page - 1) * per_page; i < page * per_page; i++)
            {
                resultList.Add(new GithubCodeLecture(result.Items[i]));
            }

            return resultList;
        } 
    }
}
