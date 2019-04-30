using CSharpFunctionalExtensions;
using EnsureThat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeMediaArcheolog : Archeolog<YoutubeMediaStudy>
    {
        private readonly IReadRepository<YoutubeMediaStudy> readRepository;
        private readonly YoutubeMediaProvider provider;
        private readonly YoutubeSettings settings;

        public YoutubeMediaArcheolog(
            IReadRepository<YoutubeMediaStudy> readRepository,
            IWriteRepository<YoutubeMediaStudy> writeRepository,
            IMessageBus bus,
            YoutubeMediaProvider provider,
            YoutubeSettings settings)
            :base(writeRepository, bus)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(settings);

            this.readRepository = readRepository;
            this.provider = provider;
            this.settings = settings;
        }

        protected override Task<Result<IEnumerable<YoutubeMediaStudy>>> GetStudies(string topic) => this.GetStudies(topic, string.Empty);

        private async Task<Result<IEnumerable<YoutubeMediaStudy>>> GetStudies(string topic, string page, int depth = 1)
        {
            var depthExceededResult = Result.Create(depth <= this.settings.MaxDepth, "Maximum youtube depth exceeded");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic, page))
                .Ensure(x => x.Items.Any(), "No youtube items for requested topic");

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<YoutubeMediaStudy>>(studiesResult.Error);
            }

            var studiesIds = studiesResult.Value.Items.Select(d => d.Id.VideoId);
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => studiesResult.Value.Items.Where(i => discoveredResourcesResult.Value.All(yr => yr.Id != i.Id.VideoId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => new YoutubeMediaStudy(x)))
                .OnFailureCompensate(() => GetStudies(topic, studiesResult.Value.NextPageToken, depth+1));
        }
    }
}