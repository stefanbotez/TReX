﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using TReX.Discovery.Shared.Business;
using TReX.Discovery.Shared.Business.Commands;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Shared.Domain;

namespace TReX.Discovery.Shared.Archeology
{
    public abstract class Archeologist<TLecture, TResource> : IArcheologist
        where TLecture : AggregateRoot, ILecture<TResource>
    {
        protected readonly IWriteRepository<TLecture> writeRepository;
        protected readonly IMessageBus bus;

        protected Archeologist(IWriteRepository<TLecture> writeRepository, IMessageBus bus)
        {
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(bus);
            this.writeRepository = writeRepository;
            this.bus = bus;
        }

        public async Task<Result> Study(StudyCommand command)
        {
            var studiesResult = await GetLectures(command.Topic);
            var discovery = new Domain.Discovery(command.DiscoveryId, command.Topic);

            return await studiesResult.OnSuccess(s => PersistStudies(s))
                .OnSuccess(() => PublishStudies(discovery, studiesResult.Value));
        }

        protected abstract Task<Result<IEnumerable<TLecture>>> GetLectures(string topic);

        protected async Task<Result> PersistStudies(IEnumerable<TLecture> studies)
        {
            var storeTasks = studies.Select(s => Extensions.TryAsync(() => this.writeRepository.CreateAsync(s)));
            var storeResults = await Task.WhenAll(storeTasks);

            return Result.Combine(storeResults);
        }

        protected async Task<Result> PublishStudies(Domain.Discovery discovery, IEnumerable<TLecture> studies)
        {
            var messages = studies.Select(s => s.ToResource())
                .Where(r => r.IsSuccess)
                .Select(r => GetDiscoveryEvent(discovery, r.Value));
            return await this.bus.PublishMessages(messages);
        }

        protected abstract IDomainEvent GetDiscoveryEvent(Domain.Discovery discovery, TResource resource);
    }
}