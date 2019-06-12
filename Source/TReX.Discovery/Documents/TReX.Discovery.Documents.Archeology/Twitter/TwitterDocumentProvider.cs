using EnsureThat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TReX.Discovery.Documents.Archeology.Twitter
{
    public class TwitterDocumentProvider
    {
        private readonly TwitterSettings settings;
        public TwitterDocumentProvider(TwitterSettings settings)
        {
            EnsureArg.IsNotNull(settings);
            this.settings = settings;
            Auth.SetUserCredentials(settings.ConsumerKey, settings.ConsumerSecret, settings.ApiKey, settings.ApiSecret);
        }


        public async Task<IEnumerable<ITweet>> Search(string query)
        {
            var searchParameter = new SearchTweetsParameters(query)
            {
                SearchType = SearchResultType.Mixed,
                MaximumNumberOfResults = 100
            };
            return await SearchAsync.SearchTweets(searchParameter);
        }

        public List<TwitterDocumentLecture> ToTwitterDocumentLecture(IEnumerable<ITweet> results, int page, int per_page)
        {
            List<TwitterDocumentLecture> resultList = new List<TwitterDocumentLecture>();
            List<ITweet> tweetsList = results.ToList();
            for (int i = (page - 1) * per_page; i < page * per_page && i < tweetsList.Count; i++)
            {
                resultList.Add(new TwitterDocumentLecture(tweetsList[i].IdStr, tweetsList[i].CreatedBy.Name, tweetsList[i].Text, tweetsList[i].CreatedAt));
            }

            return resultList;
        }
    }
}
