using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace TReX.Discovery.Media.Worker
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildContainer()
                .Resolve<Application>()
                .Run();

            Console.ReadLine();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WorkerContainer>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.RegisterInstance(configuration)
                .As<IConfiguration>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
