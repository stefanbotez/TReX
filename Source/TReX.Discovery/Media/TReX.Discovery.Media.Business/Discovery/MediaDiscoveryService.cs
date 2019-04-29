using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Media.Domain;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Business.Discovery
{
    public sealed class MediaDiscoveryService : IMediaDiscoveryService
    {
        private readonly IEnumerable<IMediaArcheolog> archeologs;
        private readonly IUnitOfWork unitOfWork;

        public MediaDiscoveryService(IEnumerable<IMediaArcheolog> archeologs, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(archeologs);
            EnsureArg.IsNotNull(unitOfWork);
            this.archeologs = archeologs;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Discover(string topic)
        {
            var studyTasks = this.archeologs.Select(a => a.Study(topic));
            var studyResults = await Task.WhenAll(studyTasks);

            var overallStudyResult = studyResults.All(s => s.IsFailure) ? Result.Fail("Discovery failed") : Result.Ok();
            return await overallStudyResult.OnSuccess(() => this.unitOfWork.CommitAsync());
        }
    }
}