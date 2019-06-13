using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Domain.Resources;
using TReX.App.Museum.Events;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Museum.EventHandlers
{
    public sealed class CodeResourceDiscoveredEventHandler : INotificationHandler<CodeResourceDiscovered>
    {
        private readonly ILogger logger;
        private readonly IWriteRepository<Resource> writeRepository;
        private readonly IUnitOfWork unitOfWork;

        public CodeResourceDiscoveredEventHandler(ILogger logger, IWriteRepository<Resource> writeRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.logger = logger;
            this.writeRepository = writeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CodeResourceDiscovered notification, CancellationToken cancellationToken)
        {
            await logger.Log($"App discovered code resource: {notification.Title}");
            await ParentDiscovery.Create(notification.DiscoveryId, notification.DiscoveryTopic)
                .OnSuccess(pd => Resource.CreateCode(notification.ProviderDetails, pd, notification.Title, notification.Description))
                .OnSuccess(r => this.writeRepository.CreateAsync(r))
                .OnSuccess(() => this.unitOfWork.CommitAsync());
        }
    }
}