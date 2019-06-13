using Autofac;
using TReX.Discovery.Code.Archeology.Github;
using TReX.Kernel.Utilities;

namespace TReX.Discovery.Code.DependencyInjection
{
    public sealed class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSettings(gitSection => new GithubSettings(
                int.Parse(gitSection[nameof(GithubSettings.MaxDepth)]),
                int.Parse(gitSection[nameof(GithubSettings.PerPage)])));
        }
    }
}