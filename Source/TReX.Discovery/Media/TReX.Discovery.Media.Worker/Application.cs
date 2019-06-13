using System.Threading.Tasks;
using EnsureThat;
using TReX.Discovery.Shared.Integration.Events;
using TReX.Kernel.Shared.Bus;

namespace TReX.Discovery.Media.Worker
{
    public sealed class Application
    {
        private readonly IMessageBus bus;

        public Application(IMessageBus bus)
        {
            EnsureArg.IsNotNull(bus);
            this.bus = bus;
        }

        public async Task Run()
        {
            await this.bus.SubscribeTo<DiscoveryCreated>();
        }
    }
}