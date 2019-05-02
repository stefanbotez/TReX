using Autofac;
using TReX.Discovery.Media.DependencyInjection;
using TReX.Kernel.Utilities;
using Module = Autofac.Module;

namespace TReX.Discovery.Media.Worker
{
    public sealed class WorkerContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacContainer>();
            builder.RegisterType<Application>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterMediatr();
        }
    }
}