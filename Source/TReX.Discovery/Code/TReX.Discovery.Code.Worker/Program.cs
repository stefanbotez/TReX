using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace TReX.Discovery.Code.Worker
{
    public static class Program
    {
        private static readonly ManualResetEvent _quitEvent = new ManualResetEvent(false);

        public static async Task Main(string[] args)
        {
            await BuildContainer()
                .Resolve<Application>()
                .Run();

            Console.WriteLine("Code worker is up..");
            Console.CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };
            _quitEvent.WaitOne();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WorkerContainer>();
            return builder.Build();
        }
    }
}
