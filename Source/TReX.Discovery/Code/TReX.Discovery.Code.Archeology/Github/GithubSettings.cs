using EnsureThat;

namespace TReX.Discovery.Code.Archeology.Github
{
    public class GithubSettings
    {
        public int MaxDepth { get; }
        public int PerPage { get; }

        public GithubSettings(int maxDepth, int perPage)
        {
            EnsureArg.IsGte(maxDepth, 1);
            EnsureArg.IsGte(perPage, 1);

            MaxDepth = maxDepth;
            PerPage = perPage;
        }
    }
}
