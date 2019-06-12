
using CSharpFunctionalExtensions;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TReX.Discovery.Code.Domain;
using TReX.Discovery.Shared.Archeology;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Code.Archeology.Github
{
    public class GithubCodeArcheologist : Archeologist<GithubCodeLecture, CodeResource>
    {
        private readonly IReadRepository<GithubCodeLecture> readRepository;
        private readonly GithubCodeProvider provider;
        private readonly GithubSettings settings;

        public GithubCodeArcheologist(
            IReadRepository<GithubCodeLecture> readRepository,
            IWriteRepository<GithubCodeLecture> writeRepository,
            IMessageBus bus,
            GithubCodeProvider provider,
            GithubSettings settings)
            : base(writeRepository, bus)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(settings);

            this.readRepository = readRepository;
            this.provider = provider;
            this.settings = settings;
        }

        protected override Task<Result<IEnumerable<GithubCodeLecture>>> GetLectures(string topic) => this.GetLectures(topic, "1");

        protected override IDomainEvent GetDiscoveryEvent(Shared.Domain.Discovery discovery, CodeResource resource) => new CodeResourceDiscovered(discovery, resource);

        private async Task<Result<IEnumerable<GithubCodeLecture>>> GetLectures(string topic, string page = "1", int depth = 1)
        {
            var depthExceededResult = Result.Create(depth <= this.settings.MaxDepth, $"Maximum github depth exceeded for topic {topic}");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic))
                .Ensure(x => provider.ToGithubCodeLecture(x, Int32.Parse(page), this.settings.PerPage).Count > 0, "No github items for requested topic");

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<GithubCodeLecture>>(studiesResult.Error);
            }
            var studiesIds = provider.ToGithubCodeLecture(studiesResult.Value, Int32.Parse(page), this.settings.PerPage).Select(o => o.RepositoryId).ToList();
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => provider.ToGithubCodeLecture(studiesResult.Value, Int32.Parse(page), this.settings.PerPage).Where(i => discoveredResourcesResult.Value.All(yr => yr.RepositoryId != i.RepositoryId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => x))
                .OnFailureCompensate(() => GetLectures(topic, page + 1, depth + 1));
        }
    }
}
