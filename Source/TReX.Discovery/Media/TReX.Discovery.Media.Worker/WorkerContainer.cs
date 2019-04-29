using System;
using System.Reflection;
using Autofac;
using MediatR;
using TReX.Discovery.Media.DependencyInjection;
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

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsClosedTypeOf(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}