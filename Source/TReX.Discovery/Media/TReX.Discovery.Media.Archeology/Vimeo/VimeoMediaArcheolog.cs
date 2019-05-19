﻿using CSharpFunctionalExtensions;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Archeology.Vimeo
{
    class VimeoMediaArcheolog : Archeolog<VimeoMediaLecture>
    {
        private readonly IReadRepository<VimeoMediaLecture> readRepository;
        private readonly VimeoMediaProvider provider;
        private readonly VimeoSettings settings;

        public VimeoMediaArcheolog(
            IReadRepository<VimeoMediaLecture> readRepository,
            IWriteRepository<VimeoMediaLecture> writeRepository,
            IMessageBus bus,
            VimeoMediaProvider provider,
            VimeoSettings settings)
            : base(writeRepository, bus)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(settings);

            this.readRepository = readRepository;
            this.provider = provider;
            this.settings = settings;
        }

        protected override Task<Result<IEnumerable<VimeoMediaLecture>>> GetLectures(string topic) => this.GetLectures(topic, string.Empty);

        private async Task<Result<IEnumerable<VimeoMediaLecture>>> GetLectures(string topic, string page, int depth = 1)
        {
            var depthExceededResult = Result.Create(depth <= this.settings.MaxDepth, $"Maximum vimeo depth exceeded for topic {topic}");

            var studiesResult = await depthExceededResult.OnSuccess(() => this.provider.Search(topic, page))
                .Ensure(x => provider.ToVimeoMediaLecture(x.Content.ReadAsStringAsync().Result).Count > 0, "No vimeo items for requested topic");

            if (studiesResult.IsFailure)
            {
                return Result.Fail<IEnumerable<VimeoMediaLecture>>(studiesResult.Error);
            }
            var studiesIds = provider.ToVimeoMediaLecture(studiesResult.Value.Content.ReadAsStringAsync().Result).Select(o => o.Value.VideoId).ToList();
            var discoveredResourcesResult = await this.readRepository.GetByIdsAsync(studiesIds);

            return await Result.Combine(studiesResult, discoveredResourcesResult)
                .OnSuccess(() => provider.ToVimeoMediaLecture(studiesResult.Value.Content.ToString()).Where(i => discoveredResourcesResult.Value.All(yr => yr.VideoId != i.Value.VideoId)))
                .Ensure(itd => itd.Any(), "No new items")
                .OnSuccess(itd => itd.Select(x => x.Value))
                .OnFailureCompensate(() => GetLectures(topic, page + 1, depth + 1));
        }
    }
}
