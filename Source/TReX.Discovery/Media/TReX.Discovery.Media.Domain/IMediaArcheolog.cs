using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Discovery.Media.Domain
{
    public interface IMediaArcheolog
    {
        Task<Result> Study(string topic);
    }
}