namespace Thor.Core.Services
{
    using System;
    using System.Threading.Tasks;

    public interface ILogServiceExtend
    {
        Task WriteExceptionSentryErrorAsync(Exception exception);

        Task WriteMessageSentryErrorAsync(string logMessage);

        void WriteLogFileError(string logMessage);

        void WriteLogFileWarning(string logMessage);

        void WriteLogFileInfo(string logMessage);
    }
}
