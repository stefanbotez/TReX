using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Domain;
using TReX.App.Museum.Events;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Museum.EventHandlers
{
    public sealed class DiscoveryCompletedEventHandler : INotificationHandler<DiscoverySucceeded>
    {
        private readonly IReadRepository<Discovery> readRepository;
        private readonly IUnitOfWork unitOfWork;

        public DiscoveryCompletedEventHandler(IReadRepository<Discovery> readRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.readRepository = readRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DiscoverySucceeded notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);

            await this.readRepository.GetByIdAsync(notification.DiscoveryId).ToResult(MuseumMessages.DiscoveryNotFound)
                .OnSuccess(d => d.AcknowledgeCompletion())
                .OnSuccess(_ => this.unitOfWork.CommitAsync());
        }
    }
}