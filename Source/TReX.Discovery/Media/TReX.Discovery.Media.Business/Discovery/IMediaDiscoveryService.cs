using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Discovery.Media.Business.Discovery
{
    public interface IMediaDiscoveryService
    {
        Task<Result> Discover(string topic);
    }
}