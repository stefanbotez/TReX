using System.Reflection;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public static class CheckpointFactory
    {
        public static string GetStream<T>()
            where T : IBusMessage
        {
            var consumerName = Assembly.GetEntryAssembly().GetName().Name;
            return $"{consumerName}-{typeof(T).Name}-Checkpoint";
        }
    }
}