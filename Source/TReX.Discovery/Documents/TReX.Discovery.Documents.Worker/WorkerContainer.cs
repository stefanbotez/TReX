using Autofac;
using TReX.Discovery.Documents.DependencyInjection;
using TReX.Discovery.Shared.Integration;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Documents.Worker
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