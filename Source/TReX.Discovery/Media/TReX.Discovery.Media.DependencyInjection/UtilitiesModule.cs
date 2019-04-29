using Autofac;
using TReX.Kernel.Shared.Bus;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class UtilitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MockBus>()
                .As<IMessageBus>()
                .InstancePerLifetimeScope();
        }
    }
}