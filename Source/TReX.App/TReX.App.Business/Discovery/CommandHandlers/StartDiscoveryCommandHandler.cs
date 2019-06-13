using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.App.Business.Discovery.Commands;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Discovery.CommandHandlers
{
    public sealed class StartDiscoveryCommandHandler : IRequestHandler<StartDiscoveryCommand, Result>
    {
        private readonly IWriteRepository<Domain.Discovery.Discovery> writeRepository;
        private readonly IUnitOfWork unitOfWork;

        public StartDiscoveryCommandHandler(IWriteRepository<Domain.Discovery.Discovery> writeRepository, IUnitOfWork unitOfWork)
        {
            EnsureArg.IsNotNull(writeRepository);
            EnsureArg.IsNotNull(unitOfWork);
            this.writeRepository = writeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(StartDiscoveryCommand command, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(command);

            return await Domain.Discovery.Discovery.CreateOnBehalfOf(command.Behalf, command.Topic)
                .OnSuccess(d => this.writeRepository.CreateAsync(d))
                .OnSuccess(() => this.unitOfWork.CommitAsync());
        }
    }
}