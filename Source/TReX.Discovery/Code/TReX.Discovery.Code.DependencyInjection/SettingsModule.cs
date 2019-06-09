using Autofac;
using Microsoft.Extensions.Configuration;
using TReX.Discovery.Code.Archeology.Github;

namespace TReX.Discovery.Code.DependencyInjection
{
    public sealed class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var githubSection = configuration.GetSection("GithubSettings");
                return new GithubSettings(int.Parse(githubSection[nameof(GithubSettings.MaxDepth)]),
                    int.Parse(githubSection[nameof(GithubSettings.PerPage)]));
            }).SingleInstance();
        }
    }
}