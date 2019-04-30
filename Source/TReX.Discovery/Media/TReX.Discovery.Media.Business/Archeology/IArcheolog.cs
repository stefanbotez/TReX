using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TReX.Discovery.Media.Business.Archeology.Commands;

namespace TReX.Discovery.Media.Business.Archeology
{
    public interface IArcheolog
    {
        Task<Result> Study(StudyCommand command);
    }
}