using System;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public static class TopicFactory
    {
        public static string GetTopic<T>(T message)
            where T : IBusMessage
        {
            return GetTopic(message.GetType());
        }

        public static string GetTopic(Type messageType)
        {
            return $"{messageType.Name}-Topic";
        }
    }
}