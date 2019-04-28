using Autofac;
using TReX.Discovery.Kernel.Raven;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RavenStoreHolder>().SingleInstance();
            builder.Register(context =>
            {
                var store = context.Resolve<RavenStoreHolder>().Store;
                return store.OpenAsyncSession();
            }).InstancePerLifetimeScope();
        }
    }
}