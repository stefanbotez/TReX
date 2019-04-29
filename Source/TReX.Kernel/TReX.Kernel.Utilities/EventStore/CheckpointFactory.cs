using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public static class CheckpointFactory
    {
        public static string GetStream<T>()
            where T : IBusMessage
        {
            return $"{typeof(T).Name}-Checkpoint";
        }
    }
}