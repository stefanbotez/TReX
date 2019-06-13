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

        protected override Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic) => this.GetLectures(topic, 1);

        protected override IDomainEvent GetDiscoveryEvent(Shared.Domain.Discovery discovery, DocumentResource resource) => new DocumentResourceDiscovered(discovery, resource);

        private async Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic, int depth = 1)
        {
            var depthExceededResult = Result.Create(depth <= this.settings.MaxDepth, $"Maximum wikipedia depth exceeded for topic {topic}");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic))
                .Ensure(x => provider.ToWikipediaDocumentLectures(x.Content.ReadAsStringAsync().Result).Count > 0, "No wikipedia items for requested topic");

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<WikipediaDocumentLecture>>(studiesResult.Error);
            }
            var studiesIds = provider.ToWikipediaDocumentLectures(studiesResult.Value.Content.ReadAsStringAsync().Result).Select(o => o.Id).ToList();
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => provider.ToWikipediaDocumentLectures(studiesResult.Value.Content.ReadAsStringAsync().Result).Where(i => discoveredResourcesResult.Value.All(yr => yr.Id != i.Id)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => x))
                .OnFailureCompensate(() => GetLectures(topic, depth + 1));
        }
    }
}
