using EnsureThat;

namespace TReX.Kernel.Utilities.EventStore
{
    public sealed class EventStoreSettings
    {
        public EventStoreSettings(string username, string password)
        {
            EnsureArg.IsNotNullOrWhiteSpace(username);
            EnsureArg.IsNotNullOrWhiteSpace(password);
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}