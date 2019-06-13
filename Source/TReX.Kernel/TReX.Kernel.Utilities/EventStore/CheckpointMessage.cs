using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public sealed class CheckpointMessage : IBusMessage
    {
        public CheckpointMessage(long lastProcessedPosition)
        {
            LastProcessedPosition = lastProcessedPosition;
        }

        public long LastProcessedPosition { get; private set; }
    }
}