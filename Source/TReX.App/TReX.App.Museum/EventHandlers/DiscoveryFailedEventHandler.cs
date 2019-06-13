using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Domain.Discovery;
using TReX.App.Museum.Events;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Museum.EventHandlers
{
    public sealed class DiscoveryFailedEventHandler : INotificationHandler<DiscoveryFailed>
    {
        private readonly ILogger logger;
        private readonly IReadRepository<Discovery> readRepository;
        private readonly IUnitOfWork unitOfWork;

        public DiscoveryFailedEventHandler(ILogger logger, IReadRepository<Discovery> readRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.logger = logger;
            this.readRepository = readRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DiscoveryFailed notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);

            await this.readRepository.GetByIdAsync(notification.DiscoveryId).ToResult(MuseumMessages.DiscoveryNotFound)
                .OnSuccess(d => d.AcknowledgeFailure())
                .OnSuccess(_ => this.logger.Log(string.Format(MuseumMessages.DiscoveryFailed, notification.Reason)))
                .OnSuccess(_ => this.unitOfWork.CommitAsync());
        }
    }
}