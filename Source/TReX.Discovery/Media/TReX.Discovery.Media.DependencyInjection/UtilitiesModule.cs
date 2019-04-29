using Autofac;
using TReX.Discovery.Kernel.Shared;
using TReX.Discovery.Kernel.Utilities;

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