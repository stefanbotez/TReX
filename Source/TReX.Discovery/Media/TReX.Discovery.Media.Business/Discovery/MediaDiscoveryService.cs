﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Media.Domain;

namespace TReX.Discovery.Media.Business.Discovery
{
    public sealed class MediaDiscoveryService : IMediaDiscoveryService
    {
        private readonly IEnumerable<IMediaArcheolog> archeologs;

        public MediaDiscoveryService(IEnumerable<IMediaArcheolog> archeologs)
        {
            EnsureArg.IsNotNull(archeologs);
            this.archeologs = archeologs;
        }

        public async Task<Result> Discover(string topic)
        {
            var studyTasks = this.archeologs.Select(a => a.Study(topic));
            var studyResults = await Task.WhenAll(studyTasks);

            return studyResults.All(s => s.IsFailure) ? Result.Fail("Discovery failed") : Result.Ok();
        }
    }
}