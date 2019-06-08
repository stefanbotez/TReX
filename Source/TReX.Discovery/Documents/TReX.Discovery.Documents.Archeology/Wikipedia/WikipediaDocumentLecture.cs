using TReX.Kernel.Shared.Domain;
using TReX.Discovery.Documents.Domain;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public sealed class WikipediaDocumentLecture : AggregateRoot, IDocumentLecure
    {
        private WikipediaDocumentLecture()

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
    }



}
