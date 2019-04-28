using Autofac;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class AutofacContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ArcheologyModule>();
            builder.RegisterModule<PersistenceModule>();
        }
    }
}