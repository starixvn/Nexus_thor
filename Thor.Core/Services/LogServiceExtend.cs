namespace Thor.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Fanex.Logging.Extensions;
    using Fanex.Logging.Extensions.Database;
    using Microsoft.Extensions.Logging;

    public class LogServiceExtend : ILogServiceExtend
    {
        private readonly Fanex.Logging.ILogger logger;
        private readonly ILogger<LogServiceExtend> filelogger;

        public LogServiceExtend(
            Fanex.Logging.ILogger logger,
            ILogger<LogServiceExtend> filelogger)
        {
            this.logger = logger;
            this.filelogger = filelogger;
        }

        public async Task WriteExceptionSentryErrorAsync(Exception exception)
        {
            try
            {
                var logMessage = new LogMessage(exception.Message)
                       .AddSection("Database Info", DatabaseInfoGetters.GetDbExceptionInfo(exception))
                       .AddSection("Server Info", CommonInfoGetters.GetServerInfo())
                       .AddSection("Exception Info", CommonInfoGetters.GetExceptionInfo(exception))
                       .ToString();
                await logger.ErrorAsync(logMessage);
            }
            catch (Exception ex)
            {
                filelogger.LogError(ex.ToString());
            }
        }

        public async Task WriteMessageSentryErrorAsync(string logMessage)
        {
            await logger.ErrorAsync(logMessage);
        }

        public void WriteLogFileError(string logMessage)
        {
            filelogger.LogError(logMessage);
        }

        public void WriteLogFileWarning(string logMessage)
        {
            filelogger.LogWarning(logMessage);
        }

        public void WriteLogFileInfo(string logMessage)
        {
            filelogger.LogInformation(logMessage);
        }
    }
}
