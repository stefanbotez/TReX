using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Media.Domain;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeMediaArcheolog : IMediaArcheolog
    {
        private readonly IMessageBus bus;
        private readonly IReadRepository<YoutubeMediaResource> readRepository;
        private readonly IWriteRepository<YoutubeMediaResource> writeRepository;
        private readonly YoutubeMediaProvider provider;

        public YoutubeMediaArcheolog(
            IReadRepository<YoutubeMediaResource> readRepository,
            IWriteRepository<YoutubeMediaResource> writeRepository,
            IMessageBus bus,
            YoutubeMediaProvider provider)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(bus);
            EnsureArg.IsNotNull(provider);

            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.bus = bus;
            this.provider = provider;
        }

        public async Task<Result> Study(string topic)
        {
            var studiesResult = await GetStudies(topic, string.Empty);
            return await studiesResult.OnSuccess(studies => PersistStudies(studies))
                .OnSuccess(() => PublishStudies(studiesResult.Value));
        }

        private async Task<Result<IEnumerable<YoutubeMediaResource>>> GetStudies(string topic, string page)
        {
            var studiesResult = await this.provider.Search(topic, page)
                .Ensure(x => x.Items.Any(), "No items for requested topic");
            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<YoutubeMediaResource>>(studiesResult.Error);
            }

            var studiesIds = studiesResult.Value.Items.Select(d => d.Id.VideoId);
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => studiesResult.Value.Items.Where(i => discoveredResourcesResult.Value.All(yr => yr.Id != i.Id.VideoId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => new YoutubeMediaResource(x)))
                .OnFailureCompensate(() => GetStudies(topic, studiesResult.Value.NextPageToken));
        }

        private async Task<Result> PersistStudies(IEnumerable<YoutubeMediaResource> studies)
        {
            var storeTasks = studies.Select(s => Extensions.TryAsync(() => this.writeRepository.CreateAsync(s)));
            var storeResults = await Task.WhenAll(storeTasks);

            return Result.Combine(storeResults);
        }

        private async Task<Result> PublishStudies(IEnumerable<YoutubeMediaResource> studies)
        {
            var messages = studies.Select(s => new MediaResourceDiscovered(s.ToMediaResource()));
            return await this.bus.PublishMessages(messages);
        }
    }
}