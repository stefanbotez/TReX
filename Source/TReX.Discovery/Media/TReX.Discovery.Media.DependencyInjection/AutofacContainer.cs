using Autofac;
using TReX.Kernel.Raven;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class AutofacContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRavenPersistence()
                .RegisterEventStoreBus()
                .RegisterModule<SettingsModule>()
                .RegisterModule<ArcheologyModule>();
        }
    }
}