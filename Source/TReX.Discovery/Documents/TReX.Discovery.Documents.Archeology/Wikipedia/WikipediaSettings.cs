using EnsureThat;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public sealed class WikipediaSettings
    {
        public WikipediaSettings(string srWhat, string srSearch, int srLimit, int srOffSet)
        {
            EnsureArg.IsNotNullOrWhiteSpace(srWhat);
            EnsureArg.IsNotNullOrWhiteSpace(SrSearch);
            EnsureArg.IsLte(srLimit, 10);
            EnsureArg.IsLte(srOffSet, 2);

            SrWhat = srWhat;
            SrSearch = srSearch;
            SrLimit = srLimit;
            SrOffSet = srOffSet;

        }

        public string SrWhat { get; }
        public string SrSearch { get; }
        public int SrLimit { get; }
        public int SrOffSet { get; }
    }
}
