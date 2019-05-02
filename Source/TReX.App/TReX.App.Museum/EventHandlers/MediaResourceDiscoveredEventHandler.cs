﻿using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using TReX.App.Museum.Events;
using TReX.Kernel.Shared;

namespace TReX.App.Museum.EventHandlers
{
    public sealed class MediaResourceDiscoveredEventHandler : INotificationHandler<MediaResourceDiscovered>
    {
        private readonly ILogger logger;

        public MediaResourceDiscoveredEventHandler(ILogger logger)
        {
            EnsureArg.IsNotNull(logger);
            this.logger = logger;
        }

        public async Task Handle(MediaResourceDiscovered notification, CancellationToken cancellationToken)
        {
            await logger.Log($"App discovered resource: {notification.Title}");
        }
    }
}