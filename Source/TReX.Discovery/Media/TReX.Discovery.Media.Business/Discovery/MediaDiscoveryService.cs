﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Media.Business.Archeology;
using TReX.Discovery.Media.Business.Archeology.Commands;
using TReX.Discovery.Media.Business.Discovery.Commands;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Business.Discovery
{
    public sealed class MediaDiscoveryService : IDiscoveryService
    {
        private readonly IEnumerable<IArcheolog> archeologs;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public MediaDiscoveryService(IEnumerable<IArcheolog> archeologs, IUnitOfWork unitOfWork, ILogger logger)
        {
            EnsureArg.IsNotNull(archeologs);
            EnsureArg.IsNotNull(unitOfWork);
            EnsureArg.IsNotNull(logger);

            this.archeologs = archeologs;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<Result> Discover(DiscoverCommand command)
        {
            var studyCommand = new StudyCommand(command.Topic, command.DiscoveryId);
            var studyTasks = this.archeologs.Select(a => a.Study(studyCommand));
            var studyResults = await Task.WhenAll(studyTasks);

            var failedStudies = studyResults.Where(r => r.IsFailure);
            foreach (var failedStudy in failedStudies)
            {
                await this.logger.Log(failedStudy.Error);
            }

            var successfulStudies = studyResults.Where(r => r.IsSuccess);
            return await Result.Create(successfulStudies.Any(), "Discovery failed. Check logs for more details")
                .OnSuccess(() => this.unitOfWork.CommitAsync());
        }
    }
}