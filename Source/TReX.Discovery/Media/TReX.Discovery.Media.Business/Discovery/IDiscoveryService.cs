using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TReX.Discovery.Media.Business.Discovery.Commands;

namespace TReX.Discovery.Media.Business.Discovery
{
    public interface IDiscoveryService
    {
        Task<Result> Discover(DiscoverCommand command);
    }
}