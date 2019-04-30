using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TReX.App.Business.Discovery.Events;
using TReX.Kernel.Shared.Bus;

namespace TReX.App.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UseBusSubscriptions(this IApplicationBuilder app)
        {
            var bus = app.ApplicationServices.GetService<IMessageBus>();
            await bus.SubscribeTo<MediaResourceDiscovered>();

            return app;
        }
    }
}