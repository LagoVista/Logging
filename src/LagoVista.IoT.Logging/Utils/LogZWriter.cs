using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using NLog;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Utils
{
    public class LogZWriter : ILogWriter
    {
        Logger _logger;

        public LogZWriter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Task WriteError(LogRecord record)
        {
            _logger.Error(record.Message);
            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            _logger.Info(record.Message);
            return Task.CompletedTask;
        }
    }
}
