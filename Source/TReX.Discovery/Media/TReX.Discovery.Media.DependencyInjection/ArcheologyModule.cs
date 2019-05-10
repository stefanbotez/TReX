using Autofac;
using TReX.Discovery.Media.Archeology.Youtube;
using TReX.Discovery.Media.Business;
using TReX.Discovery.Shared.Business;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<YoutubeMediaArcheolog>()
                .As<IArcheolog>()
                .InstancePerLifetimeScope();
            builder.RegisterType<YoutubeMediaProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MediaDiscoveryService>()
                .As<IDiscoveryService>()
                .InstancePerLifetimeScope();
        }
    }
}