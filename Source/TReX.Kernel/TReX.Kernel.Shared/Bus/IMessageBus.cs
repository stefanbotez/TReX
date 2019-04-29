using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Bus
{
    public interface IMessageBus
    {
        Task<Result> PublishMessages<T>(IEnumerable<T> messages)
            where T : IBusMessage;
    }
}