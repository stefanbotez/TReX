using Autofac;
using TReX.Kernel.Raven;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Utilities;

namespace TReX.App.Infrastructure
{
    public sealed class AutofacContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRavenPersistence();

            builder.RegisterType<MockBus>()
                .As<IMessageBus>()
                .InstancePerLifetimeScope();
        }
    }
}