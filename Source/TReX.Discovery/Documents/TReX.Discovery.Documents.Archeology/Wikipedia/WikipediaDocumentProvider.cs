using CSharpFunctionalExtensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public class WikipediaDocumentProvider
    {
        private readonly WikipediaSettings settings;

        public async Task<Result<HttpResponseMessage>> Search(string query, string page)
        {
            return Result.Fail<HttpResponseMessage>("Not implemented");

            //var request = "https://en.wikipedia.org//w/api.php?action=query&format=json&list=search&srsearch=" +
            //              settings.SrSearch + "&srlimit=" + settings.SrLimit + "&sroffset=" + settings.SrOffSet + "&srwhat=" +
            //              settings.SrWhat;

            //return await Result.Try(() => request.ExecuteAsync());
        }
    }
}
