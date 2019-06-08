using System.Reflection;
using Autofac;
using TReX.Discovery.Code.Archeology;

namespace TReX.Discovery.Code.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GithubCodeArcheolog>()
                .As<IArcheolog>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GithubCodeProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CodeDiscoveryService>()
                .As<IDiscoveryService>()
                .InstancePerLifetimeScope();
        }
    }
}