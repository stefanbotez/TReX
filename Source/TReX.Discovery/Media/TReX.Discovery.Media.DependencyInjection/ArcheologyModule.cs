using Autofac;
using TReX.Discovery.Media.Archeology.Youtube;
using TReX.Discovery.Media.Business.Discovery;
using TReX.Discovery.Media.Domain;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<YoutubeMediaArcheolog>()
                .As<IMediaArcheolog>()
                .InstancePerLifetimeScope();
            builder.RegisterType<YoutubeMediaProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MediaDiscoveryService>()
                .As<IMediaDiscoveryService>()
                .InstancePerLifetimeScope();
        }
    }
}