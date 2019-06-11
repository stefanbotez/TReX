using Autofac;
using TReX.Discovery.Code.DependencyInjection;
using TReX.Discovery.Shared.Integration;
using TReX.Kernel.Utilities;
using Module = Autofac.Module;

namespace TReX.Discovery.Code.Worker
{
    public sealed class WorkerContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAppSettings()
                .RegisterMediatr(typeof(SharedIntegrationLayer).Assembly)
                .RegisterModule<AutofacContainer>();
            builder.RegisterType<Application>()
                .AsSelf()
                .SingleInstance();
        }
    }
}