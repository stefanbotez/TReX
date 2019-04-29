using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Discovery.Kernel.Shared
{
    public interface IMessageBus
    {
        Task<Result> PublishMessages<T>(IEnumerable<T> messages)
            where T : IBusMessage;
    }
}