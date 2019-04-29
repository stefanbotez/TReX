using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TReX.Discovery.Media.Worker.Events;

namespace TReX.Discovery.Media.Worker.EventHandlers
{
    public sealed class DiscoveryCreatedEventHandler : INotificationHandler<DiscoveryCreated>
    {
        public async Task Handle(DiscoveryCreated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Discovery created: {notification.DiscoveryId}, {notification.Topic}");
        }
    }
}