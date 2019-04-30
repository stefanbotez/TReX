using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.Discovery.Media.Business.Discovery;
using TReX.Discovery.Media.Business.Discovery.Commands;
using TReX.Discovery.Media.Worker.Events;
using TReX.Kernel.Shared;

namespace TReX.Discovery.Media.Worker.EventHandlers
{
    public sealed class DiscoveryCreatedEventHandler : INotificationHandler<DiscoveryCreated>
    {
        private readonly ILogger logger;
        private readonly IEnumerable<IDiscoveryService> discoveryServices;

        public DiscoveryCreatedEventHandler(IEnumerable<IDiscoveryService> discoveryServices, ILogger logger)
        {
            EnsureArg.IsNotNull(discoveryServices);
            EnsureArg.IsNotNull(logger);
            this.discoveryServices = discoveryServices;
            this.logger = logger;
        }

        public async Task Handle(DiscoveryCreated notification, CancellationToken cancellationToken)
        {
            var command = new DiscoverCommand(notification.Topic, notification.DiscoveryId);
            foreach (var service in discoveryServices)
            {
                var serviceName = service.GetType().Name;

                await service.Discover(command)
                    .OnSuccess(() => this.logger.Log($"{serviceName} reported successful discover with id {notification.DiscoveryId} and topic {notification.Topic}"))
                    .OnFailure(e => this.logger.Log($"{serviceName} reported failure for discover with id {notification.DiscoveryId} and topic {notification.Topic}. Failure message: {e}") );
            }
        }
    }
}