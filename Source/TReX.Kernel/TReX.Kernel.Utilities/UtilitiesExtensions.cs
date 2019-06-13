using System;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using CSharpFunctionalExtensions;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Utilities.EventStore;

using ILogger = TReX.Kernel.Shared.ILogger;

namespace TReX.Kernel.Utilities
{
    public static class UtilitiesExtensions
    {
        public static ContainerBuilder RegisterConsoleLogger(this ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>()
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

        public static ContainerBuilder RegisterMediatr(this ContainerBuilder builder, Assembly handlersAssembly)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(handlersAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterSettings<TSettings>(this ContainerBuilder builder, Func<IConfigurationSection, TSettings> func)
        {
            builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                return func(config.GetSection(typeof(TSettings).Name));
            });

            return builder;
        }

        public static ContainerBuilder RegisterAppSettings(this ContainerBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.RegisterInstance(configuration)
                .As<IConfiguration>()
                .SingleInstance();

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