using System.Threading.Tasks;

namespace TReX.Kernel.Shared
{
    public interface ILogger
    {
        Task Log(string text);
    }
}