using Autofac;
using TReX.Kernel.Raven;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Code.DependencyInjection
{
    public sealed class AutofacContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConsoleLogger()
                .RegisterRavenPersistence()
                .RegisterEventStoreBus()
                .RegisterModule<SettingsModule>()
                .RegisterModule<ArcheologyModule>();
        }
    }
}