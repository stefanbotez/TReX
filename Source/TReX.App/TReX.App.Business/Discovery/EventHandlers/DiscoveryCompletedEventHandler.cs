using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Business.Discovery.Events;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Discovery.EventHandlers
{
    public sealed class DiscoveryCompletedEventHandler : INotificationHandler<DiscoveryCompleted>
    {
        private readonly IReadRepository<Domain.Discovery> readRepository;
        private readonly IUnitOfWork unitOfWork;

        public DiscoveryCompletedEventHandler(IReadRepository<Domain.Discovery> readRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(readRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.readRepository = readRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DiscoveryCompleted notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);

            await this.readRepository.GetByIdAsync(notification.DiscoveryId).ToResult(BusinessMessages.DiscoveryNotFound)
                .OnSuccess(d => d.AcknowledgeCompletion())
                .OnSuccess(_ => this.unitOfWork.CommitAsync());
        }
    }
}