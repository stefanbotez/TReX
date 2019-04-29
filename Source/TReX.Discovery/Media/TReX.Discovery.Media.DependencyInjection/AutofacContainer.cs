using Autofac;
using TReX.Kernel.Raven;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class AutofacContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRavenPersistence()
                .RegisterModule<SettingsModule>()
                .RegisterModule<ArcheologyModule>()
                .RegisterModule<UtilitiesModule>();
        }
    }
}