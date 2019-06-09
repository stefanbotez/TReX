using CSharpFunctionalExtensions;
using TReX.Discovery.Documents.Domain;
using TReX.Discovery.Shared.Archeology;
using TReX.Discovery.Shared.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public sealed class WikipediaDocumentLecture : AggregateRoot, ILecture<DocumentResource>
    {
        private WikipediaDocumentLecture()
        {
        }

        public WikipediaDocumentLecture(int ns, string title, int pageid, int size, int wordcount, string snippet)
        {
            NS = ns;
            Title = title;
            PageId = pageid;
            Size = size;
            WordCount = wordcount;
            Snippet = snippet;
        }

        public int NS { get; private set; }
        public string Title { get; private set; }
        public int PageId { get; private set; }
        public int Size { get; private set; }
        public int WordCount { get; private set; }
        public string Snippet { get; private set; }


        public Result<DocumentResource> ToResource()
        {
            return ProviderDetails.Create(PageId.ToString(), Constants.Wikipedia)
                .OnSuccess(pd => DocumentResource.Create(pd, Title, Snippet));
        }
    }



}
