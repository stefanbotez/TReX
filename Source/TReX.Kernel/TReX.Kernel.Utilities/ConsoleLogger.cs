using System;
using System.Threading.Tasks;
using TReX.Kernel.Shared;

namespace TReX.Kernel.Utilities
{
    public sealed class ConsoleLogger : ILogger
    {
        public Task Log(string info)
        {
            var line = $"[{DateTime.Now}] {info} {Environment.NewLine}";
            Console.WriteLine(line);
            return Task.CompletedTask;
        }

        public async Task LogError(string error)
        {
            var errorDelimiter = "-------------------ERROR-------------------";

            await Log(Environment.NewLine);
            await Log(errorDelimiter);
            await Log(error);
            await Log(errorDelimiter);
            await Log(Environment.NewLine);
        }
    }
}