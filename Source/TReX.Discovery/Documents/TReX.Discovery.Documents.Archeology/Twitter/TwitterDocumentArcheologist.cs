using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Documents.Domain;
using TReX.Discovery.Documents.Domain.Events;
using TReX.Discovery.Shared.Archeology;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Documents.Archeology.Twitter
{
    public class TwitterDocumentArcheologist : Archeologist<TwitterDocumentLecture, DocumentResource>
    {
        private readonly IReadRepository<TwitterDocumentLecture> readRepository;
        private readonly TwitterDocumentProvider provider;
        private readonly TwitterSettings settings;

        public TwitterDocumentArcheologist(
            IReadRepository<TwitterDocumentLecture> readRepository,
            IWriteRepository<TwitterDocumentLecture> writeRepository,
            IMessageBus bus,
            TwitterDocumentProvider provider,
            TwitterSettings settings)
            : base(writeRepository, bus)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(settings);

            this.readRepository = readRepository;
            this.provider = provider;
            this.settings = settings;
        }

        protected override Task<Result<IEnumerable<TwitterDocumentLecture>>> GetLectures(string topic) => this.GetLectures(topic, string.Empty);

        protected override IDomainEvent GetDiscoveryEvent(Shared.Domain.Discovery discovery, DocumentResource resource) => new DocumentResourceDiscovered(discovery, resource);

        private async Task<Result<IEnumerable<TwitterDocumentLecture>>> GetLectures(string topic, string page, int depth = 1)
        {
            var depthExceededResult = Result.Create(depth <= this.settings.MaxDepth, $"Maximum twitter depth exceeded for topic {topic}");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic))
                .Ensure(x => provider.ToTwitterDocumentLecture(x, Int32.Parse(page), this.settings.PerPage).Count > 0, "No twitter items for requested topic");

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<TwitterDocumentLecture>>(studiesResult.Error);
            }
            var studiesIds = provider.ToTwitterDocumentLecture(studiesResult.Value, Int32.Parse(page), this.settings.PerPage).Select(o => o.TweetId).ToList();
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => provider.ToTwitterDocumentLecture(studiesResult.Value, Int32.Parse(page), this.settings.PerPage).Where(i => discoveredResourcesResult.Value.All(yr => yr.TweetId != i.TweetId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => x))
                .OnFailureCompensate(() => GetLectures(topic, page + 1, depth + 1));
        }
    }
}
