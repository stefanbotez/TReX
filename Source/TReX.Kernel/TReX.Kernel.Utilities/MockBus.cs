using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities
{
    public sealed class MockBus : IMessageBus
    {
        public Task<Result> PublishMessages<T>(IEnumerable<T> messages) 
            where T : IBusMessage
        {
            return Task.FromResult(Result.Ok());
        }

        public Task<Result> PublishMessages<T>(params T[] messages) where T : IBusMessage
        {
            return Task.FromResult(Result.Ok());
        }

        public Task SubscribeTo<T>() where T : IBusMessage
        {
            return Task.CompletedTask;
        }
    }
}