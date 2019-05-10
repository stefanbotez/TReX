using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.Discovery.Media.Worker.Events;
using TReX.Discovery.Shared.Business;
using TReX.Discovery.Shared.Business.Commands;
using TReX.Discovery.Shared.Domain.Events;
using TReX.Kernel.Shared;
using TReX.Kernel.Shared.Bus;

namespace TReX.Discovery.Media.Worker.EventHandlers
{
    public sealed class DiscoveryCreatedEventHandler : INotificationHandler<DiscoveryCreated>
    {
        private readonly ILogger logger;
        private readonly IMessageBus bus;
        private readonly IEnumerable<IDiscoveryService> discoveryServices;

        public DiscoveryCreatedEventHandler(ILogger logger, IMessageBus bus, IEnumerable<IDiscoveryService> discoveryServices)
        {
            EnsureArg.IsNotNull(bus);
            EnsureArg.IsNotNull(logger);
            EnsureArg.IsNotNull(discoveryServices);
            this.bus = bus;
            this.logger = logger;
            this.discoveryServices = discoveryServices;
        }

        public async Task Handle(DiscoveryCreated notification, CancellationToken cancellationToken)
        {
            var command = new DiscoverCommand(notification.Topic, notification.DiscoveryId);

            var discoveryTasks = this.discoveryServices.Select(ds =>
            {
                var serviceName = ds.GetType().Name;

                return ds.Discover(command)
                    .OnFailure(e => this.logger.Log($"{serviceName} reported failure for discover with id {notification.DiscoveryId} and topic {notification.Topic}. Failure message: {e}"))
                    .OnSuccess(() => this.logger.Log($"{serviceName} reported successful discover with id {notification.DiscoveryId} and topic {notification.Topic}"));
            });

            await Result.Combine(await Task.WhenAll(discoveryTasks))
                .OnSuccess(() => this.bus.PublishMessages(new DiscoverySucceeded(notification.DiscoveryId)))
                .OnFailure(e => this.bus.PublishMessages(new DiscoveryFailed(notification.DiscoveryId, e)));
        }
    }
}