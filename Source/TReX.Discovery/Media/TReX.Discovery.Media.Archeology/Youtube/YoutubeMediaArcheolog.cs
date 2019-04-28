using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Raven.Client.Documents.Session;
using TReX.Discovery.Kernel.Shared;
using TReX.Discovery.Media.Business;
using TReX.Discovery.Media.Domain;

namespace TReX.Discovery.Media.Archeology.Youtube
{
    public sealed class YoutubeMediaArcheolog : IMediaArcheolog
    {
        private readonly IMessageBus bus;
        private readonly IAsyncDocumentSession session;
        private readonly YoutubeMediaProvider provider;

        public YoutubeMediaArcheolog(IMessageBus bus, IAsyncDocumentSession session, YoutubeMediaProvider provider)
        {
            EnsureArg.IsNotNull(bus);
            EnsureArg.IsNotNull(session);
            EnsureArg.IsNotNull(provider);

            this.bus = bus;
            this.session = session;
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
            var result = await this.provider.Search(topic, page);
            var hasItems = result.Ensure(x => x.Items.Any(), "No items for requested topic");
            if (hasItems.IsFailure)
            {
                return Result.Fail<IEnumerable<YoutubeMediaResource>>(hasItems.Error);
            }

            var discoveredResources = await this.session.LoadAsync<YoutubeMediaResource>(result.Value.Items.Select(i => i.Id.VideoId));
            return await hasItems.Map(lr => lr.Items.Where(i => discoveredResources.All(yr => yr.Key != i.Id.VideoId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => new YoutubeMediaResource(x)))
                .OnFailureCompensate(() => GetStudies(topic, result.Value.NextPageToken));
        }

        private async Task<Result> PersistStudies(IEnumerable<YoutubeMediaResource> studies)
        {
            var storeTasks = studies.Select(s => Extensions.TryAsync(() => this.session.StoreAsync(s, s.VideoId)));
            var storeResults = await Task.WhenAll(storeTasks);

            return await Result.Combine(storeResults)
                .OnSuccess(() => Extensions.TryAsync(() => this.session.SaveChangesAsync()));
        }

        private async Task<Result> PublishStudies(IEnumerable<YoutubeMediaResource> studies)
        {
            var messages = studies.Select(s => new MediaResourceDiscovered(s.ToMediaResource()));
            return await this.bus.PublishMessages(messages);
        }
    }
}