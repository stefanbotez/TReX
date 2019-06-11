using Autofac;
using TReX.Discovery.Code.Archeology.Github;
using TReX.Discovery.Code.Business;
using TReX.Discovery.Shared.Business;

namespace TReX.Discovery.Code.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GithubCodeArcheologist>()
                .As<IArcheologist>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GithubCodeProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CodeDiscoveryService>()
                .As<IDiscoveryService>()
                .InstancePerLifetimeScope();
        }
    }
}