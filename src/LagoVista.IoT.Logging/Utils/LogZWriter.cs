using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System.Threading.Tasks;
using NLog;
using NLog.Fluent;
using System;
using System.Security.Cryptography.X509Certificates;

namespace LagoVista.IoT.Logging.Utils
{
    public class LogZWriter : ILogWriter
    {
        Logger _logger;
        ConsoleLogWriter _consoleLogWriter = new ConsoleLogWriter();

        private readonly string _appName;
        private readonly string _version;
        private readonly string _environment;

        public LogZWriter(string version, string appName, string environment)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _appName = appName;
            _version = version;
            _environment = environment;
        }

        public Task WriteError(LogRecord record)
        {
            var msg = _logger.Error();
            msg.Message(record.Message);

            if (!String.IsNullOrEmpty(record.Tag))
            {
                if (!record.Tag.StartsWith("["))
                    record.Tag = "[" + record.Tag;

                if (!record.Tag.EndsWith("]"))
                    record.Tag += "]";

                msg.Property("nuviotTag", record.Tag);
            }
                       
            if (!String.IsNullOrEmpty(record.HostId)) msg.Property(nameof(LogRecord.HostId), record.HostId);
            if (!String.IsNullOrEmpty(record.InstanceId)) msg.Property(nameof(LogRecord.InstanceId), record.InstanceId);
            if (!String.IsNullOrEmpty(record.Area)) msg.Property(nameof(LogRecord.Area), record.Area);
            if (!String.IsNullOrEmpty(record.Version)) msg.Property("nuviotVersion", record.Version);
            if (!String.IsNullOrEmpty(record.OldState)) msg.Property(nameof(LogRecord.OldState), record.OldState);
            if (!String.IsNullOrEmpty(record.NewState)) msg.Property(nameof(LogRecord.NewState), record.NewState);
            if (!String.IsNullOrEmpty(record.StackTrace)) msg.Property("nuviotStackTrace", record.StackTrace);

            msg.Property("nuviotApp", _appName);
            msg.Property("nuviotVersion", _version);
            msg.Property("nuviotEnvironment", _environment);

            foreach (var prop in record.Parameters)
                msg.Property(prop.Key, prop.Value);

            msg.Write();

            _consoleLogWriter.WriteError(record);

            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            if (record.LogLevel?.ToLower() == "trace")
            {
                _logger.Trace(record.Message);
            }
            else
            {
                var msg = _logger.Info()
                  .Message(record.Message);

                if (!String.IsNullOrEmpty(record.Tag))
                {
                    if (!record.Tag.StartsWith("["))
                        record.Tag = "[" + record.Tag;

                    if (!record.Tag.EndsWith("]"))
                        record.Tag += "]";

                    msg.Property("nuviotTag", record.Tag);
                }

                msg.Property("nuviotApp", _appName);
                msg.Property("nuviotVersion", _version);
                msg.Property("nuviotEnvironment", _environment);


                if (!String.IsNullOrEmpty(record.HostId)) msg.Property(nameof(LogRecord.HostId), record.HostId);
                if (!String.IsNullOrEmpty(record.InstanceId)) msg.Property(nameof(LogRecord.InstanceId), record.InstanceId);
                if (!String.IsNullOrEmpty(record.Area)) msg.Property(nameof(LogRecord.Area), record.Area);
                if (!String.IsNullOrEmpty(record.Version)) msg.Property("nuviotVersion", record.Version);
                if (!String.IsNullOrEmpty(record.OldState)) msg.Property(nameof(LogRecord.OldState), record.OldState);
                if (!String.IsNullOrEmpty(record.NewState)) msg.Property(nameof(LogRecord.NewState), record.NewState);

                foreach (var prop in record.Parameters)
                {
                    msg.Property(prop.Key, prop.Value);
                }
                    
                msg.Write();
            }
                _consoleLogWriter.WriteEvent(record);

                return Task.CompletedTask;            
        }
    }
}
