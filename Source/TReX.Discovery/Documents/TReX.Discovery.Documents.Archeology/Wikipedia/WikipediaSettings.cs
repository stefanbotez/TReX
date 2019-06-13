namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public sealed class WikipediaSettings
    {
        public WikipediaSettings(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        public int MaxDepth { get; }
    }
}
