using System;
using System.Collections.Generic;
using System.Drawing;
using CSharpFunctionalExtensions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Newtonsoft.Json.Linq;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public class WikipediaDocumentProvider
    {
        private readonly WikipediaSettings settings;

        public async Task<HttpResponseMessage> Search(string query)
        {
            HttpClient client = new HttpClient();
             var request = "https://en.wikipedia.org//w/api.php?action=query&format=json&list=search&srsearch=" +
                            query + "&srlimit=" + settings.SrLimit + "&sroffset=" + settings.SrOffSet + "&srwhat=" +
                            settings.SrWhat;

             return await client.GetAsync(request);
        }

        public List<WikipediaDocumentLecture> ToWikipediaDocumentLectures(string json)
        {
            List<Result<WikipediaDocumentLecture>> results = new List<Result<WikipediaDocumentLecture>>();
            JToken token = JToken.Parse(json);
            JArray data = (JArray)token.SelectToken("search");
            foreach (JToken wikiData in data)
            {
                var NS = wikiData["ns"].ToString();
                var Title = wikiData["title"].ToString();
                var PageId = wikiData["pageid"];
                var Size = wikiData["size"];
                var WordCount = wikiData["wordcount"];
                var Snippet = wikiData["snippet"].ToString();
                var Timestamp = wikiData["timestamp"].ToString();

            }

            results.Add(t => new WikipediaDocumentLecture(NS, title, PageId, Size, wordcount, Snippet, timestamp));
            return results;
        }


    }
}
