using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TReX.Kernel.Shared;

namespace TReX.Kernel.Utilities
{
    public sealed class Logger : ILogger
    {
        private readonly string logsPath;

        public Logger(IConfiguration configuration)
        {
            this.logsPath = configuration["LogsPath"];
            Directory.CreateDirectory(this.logsPath);
        }

        public async Task Log(string info)
        {
            var today = DateTime.Now.ToString("dd-MM-yyyy");
            var logFilePath = $"{this.logsPath}/{today}-log.txt";

            var line = $"[{DateTime.Now}] {info} {Environment.NewLine}";

           await Extensions.TryAsync(() => File.AppendAllTextAsync(logFilePath, line));
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