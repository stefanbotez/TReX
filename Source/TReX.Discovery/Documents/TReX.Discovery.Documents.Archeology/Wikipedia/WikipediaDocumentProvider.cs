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
             var request = "https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch=" + query +
                           "&utf8=&format=json";

            return await client.GetAsync(request);
        }

        public List<WikipediaDocumentLecture> ToWikipediaDocumentLectures(string json)
        {
            List<WikipediaDocumentLecture> results = new List<WikipediaDocumentLecture>();
            JToken token = JToken.Parse(json);
            JArray data = (JArray)token["query"].SelectToken("search");
            foreach (JToken wikiData in data)
            {
                int NS = (int) wikiData["ns"];
                string Title = wikiData["title"].ToString();
                int PageId = (int) wikiData["pageid"];
                int Size = (int) wikiData["size"];
                int WordCount = (int) wikiData["wordcount"];
                string Snippet = wikiData["snippet"].ToString();
                string Timestamp = wikiData["timestamp"].ToString();

                results.Add(new WikipediaDocumentLecture(NS, Title, PageId, Size, WordCount, Snippet, Timestamp));
    
            }

            return results;


        }

        }
    }

