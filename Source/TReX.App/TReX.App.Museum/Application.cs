using System.Threading.Tasks;
using EnsureThat;
using TReX.App.Museum.Events;
using TReX.Kernel.Shared.Bus;

namespace TReX.App.Museum
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
            await this.bus.SubscribeTo<MediaResourceDiscovered>();
            await this.bus.SubscribeTo<DiscoverySucceeded>();
            await this.bus.SubscribeTo<DiscoveryFailed>();
        }
    }
}