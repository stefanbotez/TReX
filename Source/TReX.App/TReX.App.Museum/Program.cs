using System;
using System.Threading.Tasks;
using Autofac;

namespace TReX.App.Museum
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
            builder.RegisterModule<MuseumContainer>();

            return builder.Build();
        }
    }
}
