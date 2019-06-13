using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TReX.Discovery.Shared.Business.Commands;

namespace TReX.Discovery.Shared.Business
{
    public interface IArcheologist
    {
        Task<Result> Study(StudyCommand command);
    }
}