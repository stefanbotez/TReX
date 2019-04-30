using System.Text;
using Autofac;
using CSharpFunctionalExtensions;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Utilities.EventStore;

using ILogger = TReX.Kernel.Shared.ILogger;

namespace TReX.Kernel.Utilities
{
    public static class UtilitiesExtensions
    {
        public static ContainerBuilder RegisterLogger(this ContainerBuilder builder)
        {
            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterEventStoreBus(this ContainerBuilder builder)
        {
            builder.RegisterType<EventStoreSubscriptionFactory>()
                .AsSelf()
                .InstancePerLifetimeScope();

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

        internal static Maybe<T> ToDecodedMessage<T>(this RecordedEvent @event)
            where T : class
        {
            var jsonBody = Encoding.UTF8.GetString(@event.Data);
            return JsonConvert.DeserializeObject<T>(jsonBody);
        }
    }
}