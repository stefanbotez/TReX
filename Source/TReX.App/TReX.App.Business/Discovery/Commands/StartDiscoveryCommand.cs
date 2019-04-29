using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Business.Discovery.Commands
{
    public sealed class StartDiscoveryCommand : IRequest<Result>
    {
        public StartDiscoveryCommand(string topic, Behalf behalf)
        {
            EnsureArg.IsNotNullOrWhiteSpace(topic);
            EnsureArg.IsNotNull(behalf);
            Topic = topic;
            Behalf = behalf;
        }

        public string Topic { get; }

        public Behalf Behalf { get; }
    }
}