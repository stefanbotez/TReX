using Autofac;
using TReX.Discovery.Media.Archeology.Vimeo;
using TReX.Discovery.Media.Archeology.Youtube;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Media.DependencyInjection
{
    public sealed class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSettings(youtubeSection => new YoutubeSettings(
                    youtubeSection[nameof(YoutubeSettings.ApiKey)],
                    youtubeSection[nameof(YoutubeSettings.AppName)],
                    youtubeSection[nameof(YoutubeSettings.RequestPart)],
                    youtubeSection[nameof(YoutubeSettings.ResourceType)],
                    int.Parse(youtubeSection[nameof(YoutubeSettings.MaxResults)]),
                    int.Parse(youtubeSection[nameof(YoutubeSettings.MaxDepth)])))

                .RegisterSettings(vimeoSection => new VimeoSettings(vimeoSection[nameof(VimeoSettings.AccessToken)],
                    int.Parse(vimeoSection[nameof(VimeoSettings.MaxDepth)]),
                    int.Parse(vimeoSection[nameof(VimeoSettings.PerPage)])));
        }
    }
}