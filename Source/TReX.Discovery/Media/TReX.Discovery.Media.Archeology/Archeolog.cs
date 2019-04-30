using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Media.Business.Archeology;
using TReX.Discovery.Media.Business.Archeology.Commands;
using TReX.Discovery.Media.Domain;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Media.Archeology
{
    public abstract class Archeolog<TStudy> : IArcheolog
        where TStudy : AggregateRoot, IMediaStudy
    {
        protected readonly IWriteRepository<TStudy> writeRepository;
        protected readonly IMessageBus bus;

        protected Archeolog(IWriteRepository<TStudy> writeRepository, IMessageBus bus)
        {
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(bus);
            this.writeRepository = writeRepository;
            this.bus = bus;
        }

        public async Task<Result> Study(StudyCommand command)
        {
            var studiesResult = await GetStudies(command.Topic);
            return await studiesResult.OnSuccess(PersistStudies)
                .OnSuccess(() => PublishStudies(command.DiscoveryId, studiesResult.Value));
        }

        protected abstract Task<Result<IEnumerable<TStudy>>> GetStudies(string topic);

        protected async Task<Result> PersistStudies(IEnumerable<TStudy> studies)
        {
            var storeTasks = studies.Select(s => Extensions.TryAsync(() => this.writeRepository.CreateAsync(s)));
            var storeResults = await Task.WhenAll(storeTasks);

            return Result.Combine(storeResults);
        }

        protected async Task<Result> PublishStudies(string discoveryId, IEnumerable<TStudy> studies)
        {
            var messages = studies.Select(s => new MediaResourceDiscovered(discoveryId, s.ToMediaResource()));
            return await this.bus.PublishMessages(messages);
        }
    }
}