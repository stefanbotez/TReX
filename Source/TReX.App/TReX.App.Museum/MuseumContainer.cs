using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using TReX.Kernel.Raven;
using TReX.Kernel.Utilities;
using Module = Autofac.Module;

namespace TReX.App.Museum
{
    public sealed class MuseumContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatr(Assembly.GetExecutingAssembly())
                .RegisterRavenPersistence()
                .RegisterEventStoreBus()
                .RegisterLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.RegisterInstance(configuration)
                .As<IConfiguration>()
                .SingleInstance();

            builder.RegisterType<Application>()
                .AsSelf()
                .SingleInstance();
        }
    }
}