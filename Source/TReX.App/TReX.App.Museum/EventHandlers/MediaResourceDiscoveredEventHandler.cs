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
    public sealed class MediaResourceDiscoveredEventHandler : INotificationHandler<MediaResourceDiscovered>
    {
        private readonly ILogger logger;
        private readonly IWriteRepository<Resource> writeRepository;
        private readonly IUnitOfWork unitOfWork;

        public MediaResourceDiscoveredEventHandler(ILogger logger, IWriteRepository<Resource> writeRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.logger = logger;
            this.writeRepository = writeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(MediaResourceDiscovered notification, CancellationToken cancellationToken)
        {
            await logger.Log($"App discovered media resource: {notification.Title}");
            await ParentDiscovery.Create(notification.DiscoveryId, notification.DiscoveryTopic)
                .OnSuccess(pd => Resource.CreateMedia(notification.ProviderDetails, pd, notification.Title, notification.Description))
                .OnSuccess(r => r.FillBucket(nameof(MediaResourceDiscovered.ThumbnailUrl), notification.ThumbnailUrl))
                .OnSuccess(r => this.writeRepository.CreateAsync(r))
                .OnSuccess(() => this.unitOfWork.CommitAsync());
        }
    }
}