using Autofac;
using TReX.Discovery.Documents.Archeology.Twitter;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Documents.DependencyInjection
{
    public sealed class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSettings(twitterSection => new TwitterSettings(
                twitterSection[nameof(TwitterSettings.ConsumerKey)],
                twitterSection[nameof(TwitterSettings.ConsumerSecret)],
                twitterSection[nameof(TwitterSettings.ApiKey)],
                twitterSection[nameof(TwitterSettings.ApiSecret)],
                int.Parse(twitterSection[nameof(TwitterSettings.PerPage)]),
                int.Parse(twitterSection[nameof(TwitterSettings.MaxDepth)])));
        }
    }
}