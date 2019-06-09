using System.Collections.Generic;
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

        protected override Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic) => this.GetLectures(topic, 0);

        protected override IDomainEvent GetDiscoveryEvent(string discoveryId, DocumentResource resource) => new DocumentResourceDiscovered(discoveryId, resource);

        private async Task<Result<IEnumerable<WikipediaDocumentLecture>>> GetLectures(string topic, int srlimit, int depth = 1)
        {
            return Result.Fail<IEnumerable<WikipediaDocumentLecture>>("Not implemented");

            //var depthExceededResult = Result.Create(depth <= this.settings.SrLimit, $"Maximum wikipedia depth exceeded for topic {topic}");

            //var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic, srlimit))
            //    .Ensure(x => x.Items.Any(), "No wikipedia items for requested topic");

            //if (studiesResult.IsFailure)
            //{
            //    return Result.Fail<IEnumerable<WikipediaDocumentLecture>>(studiesResult.Error);
            //}
//
//            var studiesIds = studiesResult.Value.Items.Select(d => d.Id.VideoId);
//            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);
//
//            return await Result.Combine(studiesResult, discoveredResourcesResult)
//                .OnSuccess(() => studiesResult.Value.Items.Where(i => discoveredResourcesResult.Value.All(yr => yr.Id != i.Id.VideoId)))
//                .Ensure(itd => itd.Any(), "No new items")
//                .OnSuccess(itd => itd.Select(x => new WikipediaDocumentLecture(x)))
//                .OnFailureCompensate(() => GetLectures(topic, studiesResult.Value.NextPageToken, depth + 1));
        }
    }
}
