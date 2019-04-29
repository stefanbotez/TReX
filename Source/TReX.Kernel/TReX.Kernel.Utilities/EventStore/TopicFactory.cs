using System;
using TReX.Kernel.Shared.Bus;

namespace TReX.Kernel.Utilities.EventStore
{
    public static class TopicFactory
    {
        public static string GetTopic<T>()
            where T :IBusMessage
        {
            return GetTopic(typeof(T));
        }

        public static string GetTopic<T>(T message)
            where T : IBusMessage
        {
            return GetTopic<T>();
        }

        private static string GetTopic(Type messageType)
        {
            return $"{messageType.Name}-Topic";
        }
    }
}