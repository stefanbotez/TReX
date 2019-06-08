using CSharpFunctionalExtensions;
using EnsureThat;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TReX.Discovery.Media.Domain;

namespace TReX.Discovery.Media.Archeology.Vimeo
{
    class VimeoMediaProvider
    {
        private readonly VimeoSettings settings;
        public VimeoMediaProvider(VimeoSettings settings)
        {
            EnsureArg.IsNotNull(settings);
            this.settings = settings;
        }


        public async Task<Result<HttpResponseMessage>> Search(string query, string page = "1")
        {
            var request = "https://api.vimeo.com/videos?query=" + query + "&page=" + page + "&per_page=" + settings.PerPage;
            return await Result.Try(() => ExecuteGetCommand(request, settings.AccessToken));
        }

        public Task<HttpResponseMessage> ExecuteGetCommand(String url, String token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);

            return client.GetAsync(url);
        }

        public List<Result<VimeoMediaLecture>> ToVimeoMediaLecture(string json)
        {
            List<Result<VimeoMediaLecture>> results = new List<Result<VimeoMediaLecture>>();

            JToken token = JToken.Parse(json);
            JArray data = (JArray)token.SelectToken("data");
            foreach (JToken videoData in data)
            {
                var title = videoData["name"].ToString();
                var description = videoData["description"].ToString();
                var videoId = videoData["link"].ToString().Substring(18);
                var publishedAt = DateTime.Parse(videoData["release_time"].ToString());

                var thumbnailList = (JArray)videoData["pictures"].SelectToken("sizes");

                long maxWidth = 0;
                long MaxHeight = 0;
                string thumbnailUrl = "";

                foreach(JToken picture in thumbnailList) {
                    if (Convert.ToInt64(picture["width"]) > maxWidth && Convert.ToInt64(picture["height"]) > MaxHeight)
                    {
                        maxWidth = Convert.ToInt64(picture["width"]);
                        MaxHeight = Convert.ToInt64(picture["height"]);
                        thumbnailUrl = picture["link"].ToString();
                    }
                }

                Result<Thumbnail> thumbnailResult = Thumbnail.Create(thumbnailUrl);

                results.Add(thumbnailResult.OnSuccess(t => new VimeoMediaLecture(videoId, title, description, t, publishedAt)));
            }
            return results;
        }
    }
}
