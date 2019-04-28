using Autofac;
using Microsoft.Extensions.Configuration;
using TReX.Discovery.Kernel.Raven;
using TReX.Discovery.Media.Archeology.Youtube;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var youtubeSection = configuration.GetSection("YoutubeSettings");
                return new YoutubeSettings(youtubeSection[nameof(YoutubeSettings.ApiKey)],
                    youtubeSection[nameof(YoutubeSettings.AppName)],
                    youtubeSection[nameof(YoutubeSettings.RequestPart)],
                    youtubeSection[nameof(YoutubeSettings.ResourceType)],
                    int.Parse(youtubeSection[nameof(YoutubeSettings.MaxResults)]));
            }).SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var ravenSection = configuration.GetSection("RavenSettings");
                return new RavenSettings(ravenSection[nameof(RavenSettings.DatabaseName)],
                    ravenSection[nameof(RavenSettings.ServerUrl)]);
            }).SingleInstance();

        }
    }
}