using Autofac;
using TReX.Discovery.Media.Archeology.Vimeo;
using TReX.Discovery.Media.Archeology.Youtube;
using TReX.Discovery.Media.Business;
using TReX.Discovery.Shared.Business;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<YoutubeMediaArcheologist>()
                .As<IArcheologist>()
                .InstancePerLifetimeScope();
            builder.RegisterType<YoutubeMediaProvider>()
                .InstancePerLifetimeScope();
            //builder.RegisterType<VimeoMediaArcheologist>()
            //    .As<IArcheologist>()
            //    .InstancePerLifetimeScope();
            //builder.RegisterType<VimeoMediaProvider>()
            //    .InstancePerLifetimeScope();
            builder.RegisterType<MediaDiscoveryService>()
                .As<IDiscoveryService>()
                .InstancePerLifetimeScope();
        }
    }
}