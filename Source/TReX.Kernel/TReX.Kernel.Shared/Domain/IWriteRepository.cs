using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public interface IWriteRepository<T>
        where T : AggregateRoot
    {
        Task<Result> CreateAsync(T aggregate);
    }
}