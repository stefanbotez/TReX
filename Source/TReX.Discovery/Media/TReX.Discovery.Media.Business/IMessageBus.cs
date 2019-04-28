using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TReX.Discovery.Kernel.Shared;

namespace TReX.Discovery.Media.Business
{
    public interface IMessageBus
    {
        Task<Result> PublishMessages<T>(IEnumerable<T> messages)
            where T : IBusMessage;
    }
}