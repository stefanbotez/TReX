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

        public void SubscribeTo<T>() where T : IBusMessage
        {
        }
    }
}