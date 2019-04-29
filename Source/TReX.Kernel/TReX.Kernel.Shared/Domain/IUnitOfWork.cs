using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public interface IUnitOfWork
    {
        Task<Result> CommitAsync();
    }
}