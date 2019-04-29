using Autofac;
using Microsoft.Extensions.Configuration;
using TReX.Kernel.Shared.Domain;

namespace TReX.Kernel.Raven
{
    public static class RavenExtensions
    {
        public static ContainerBuilder RegisterRavenPersistence(this ContainerBuilder builder)
        {

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var ravenSection = configuration.GetSection("RavenSettings");
                return new RavenSettings(ravenSection[nameof(RavenSettings.DatabaseName)],
                    ravenSection[nameof(RavenSettings.ServerUrl)]);
            }).SingleInstance();


            builder.RegisterType<RavenStoreHolder>().AsSelf()
                .SingleInstance();
            builder.RegisterType<AggregateTracker>().AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(context =>
            {
                var store = context.Resolve<RavenStoreHolder>().Store;
                return store.OpenAsyncSession();
            }).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RavenReadRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RavenWriteRepository<>))
                .As(typeof(IWriteRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<RavenUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}