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
    }
}