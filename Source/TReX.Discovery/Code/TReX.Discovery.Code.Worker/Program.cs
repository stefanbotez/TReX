using System;
using System.Threading.Tasks;
using Autofac;

namespace TReX.Discovery.Code.Worker
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
            return builder.Build();
        }
    }
}
