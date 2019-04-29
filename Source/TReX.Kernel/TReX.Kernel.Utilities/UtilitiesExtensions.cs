using System;
using Autofac;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Utilities.EventStore;

namespace TReX.Kernel.Utilities
{
    public static class UtilitiesExtensions
    {
        public static ContainerBuilder RegisterEventStoreBus(this ContainerBuilder builder)
        {
            builder.RegisterType<EventStoreBus>()
                .As<IMessageBus>()
                .InstancePerLifetimeScope();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var eventStoreSection = configuration.GetSection(nameof(EventStoreSettings));
                return new EventStoreSettings(
                    eventStoreSection[nameof(EventStoreSettings.Username)],
                    eventStoreSection[nameof(EventStoreSettings.Password)]);
            }).SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var settings = context.Resolve<EventStoreSettings>();

                var connectionString = string.Format(configuration.GetConnectionString("EventStoreConn"), settings.Username, settings.Password);
                var connection = EventStoreConnection.Create(connectionString);

                connection.ConnectAsync().Wait();
                return connection;
            }).InstancePerLifetimeScope();

            return builder;
        }
    }
}