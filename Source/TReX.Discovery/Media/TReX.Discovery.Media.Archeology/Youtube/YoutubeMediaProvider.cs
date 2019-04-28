using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeMediaProvider
    {
        private readonly YoutubeSettings settings;
        private readonly YouTubeService youtubeService;

        public YoutubeMediaProvider(YoutubeSettings settings)
        {
            EnsureArg.IsNotNull(settings);
            this.settings = settings;
            this.youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = settings.ApiKey,
                ApplicationName = settings.AppName
            });
        }

        public async Task<Result<SearchListResponse>> Search(string query, string page)
        {
            var request = this.GetRequest(query, string.Empty);
            return await Result.Try(() => request.ExecuteAsync());
        }

        private SearchResource.ListRequest GetRequest(string topic, string page)
        {
            var request = this.youtubeService.Search.List(settings.RequestPart);
            request.Type = this.settings.ResourceType;
            request.MaxResults = this.settings.MaxResults;
            request.Q = topic;
            request.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            request.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            request.PageToken = page;

            return request;
        }
    }
}