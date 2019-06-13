using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReX.Discovery.Media.Archeology.Vimeo
{
    public class VimeoSettings
    {
        public string AccessToken { get; }
        public int MaxDepth { get; }
        public int PerPage { get; }

        public VimeoSettings(string accessToken, int maxDepth, int perPage)
        {
            EnsureArg.IsNotNullOrWhiteSpace(accessToken);
            EnsureArg.IsGte(maxDepth, 1);
            EnsureArg.IsGte(perPage, 1);

            AccessToken = accessToken;
            MaxDepth = maxDepth;
            PerPage = perPage;
        }
    }
}
