using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public interface IReadRepository<T>
        where T : AggregateRoot
    {
        Task<Maybe<T>> GetByIdAsync(string id);

        Task<Result<IEnumerable<T>>> GetByIdsAsync(IEnumerable<string> ids);

        Task<IEnumerable<T>> GetAll();
    }
}