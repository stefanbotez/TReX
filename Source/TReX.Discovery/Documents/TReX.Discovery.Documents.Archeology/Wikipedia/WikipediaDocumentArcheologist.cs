using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Documents.Domain;
using TReX.Discovery.Documents.Domain.Events;
using TReX.Discovery.Shared.Archeology;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Archeology.Wikipedia
{
    public class WikipediaDocumentArcheologist : Archeologist<WikipediaDocumentLecture, DocumentResource>
    {
        private readonly IReadRepository<WikipediaDocumentLecture> readRepository;
        private readonly WikipediaDocumentProvider provider;
        private readonly WikipediaSettings settings;

        public WikipediaDocumentArcheologist(
            IReadRepository<WikipediaDocumentLecture> readRepository,
            IWriteRepository<WikipediaDocumentLecture> writeRepository,
            IMessageBus bus,
            WikipediaDocumentProvider provider,
            WikipediaSettings settings)
            : base(writeRepository, bus)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(settings);

            this.readRepository = readRepository;
            this.provider = provider;
            this.settings = settings;
        }

        protected override Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic) => this.GetLectures(topic, string.Empty);

        protected override IDomainEvent GetDiscoveryEvent(string discoveryId, DocumentResource resource) => new DocumentResourceDiscovered(discoveryId, resource);

        private async Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic, string page, int depth)
        {


            var depthExceededResult = Result.Create(depth <= this.settings.SrOffSet, $"Maximum wikipedia depth exceeded for topic {topic}");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic))
                .Ensure(x => provider.ToWikipediaDocumentLectures((x, Int32.Parse(page), depthExceededResult).Count > 0, "No wiki items for requested topic")

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<WikipediaDocumentLecture>>(studiesResult.Error);
            }

            var studiesIds = provider.ToWikipediaDocumentLectures(studiesResult.Value.Content.ReadAsStringAsync().Result).Select(o => o.PageId).ToList();
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => provider.ToWikipediaDocumentLectures(studiesResult.Value.Content.ToString()).Where(i => discoveredResourcesResult(yr => yr.PageId != i.Pageid)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => x.PageId))
                .OnFailureCompensate(() => GetLectures(topic, page + 1, depth + 1));
        }
    }

}
