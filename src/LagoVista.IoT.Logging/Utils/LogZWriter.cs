using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System.Threading.Tasks;
using NLog;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
            var logEvent = new LogEventInfo()
            {
                Level = LogLevel.Error,
                Message = record.Message
            };

            if (!String.IsNullOrEmpty(record.Tag))
            {
                if (!record.Tag.StartsWith("["))
                    record.Tag = "[" + record.Tag;

                if (!record.Tag.EndsWith("]"))
                    record.Tag += "]";

                logEvent.Properties.Add("nuviotTag", record.Tag);
            }
            else
            {
                var regEx = new Regex(@"\[[\w+_<>]+\]");
                var match = regEx.Match(record.Message);
                if (match.Success)
                {
                    logEvent.Properties.Add("nuviotTag", match.Value);
                }
            }

            if (!String.IsNullOrEmpty(record.HostId)) logEvent.Properties.Add(nameof(LogRecord.HostId), record.HostId);
            if (!String.IsNullOrEmpty(record.InstanceId)) logEvent.Properties.Add(nameof(LogRecord.InstanceId), record.InstanceId);
            if (!String.IsNullOrEmpty(record.Area)) logEvent.Properties.Add(nameof(LogRecord.Area), record.Area);
            if (!String.IsNullOrEmpty(record.Version)) 
                logEvent.Properties.Add("nuviotVersion", record.Version);
            else
                logEvent.Properties.Add("nuviotVersion", _version);

            if (!String.IsNullOrEmpty(record.OldState)) logEvent.Properties.Add(nameof(LogRecord.OldState), record.OldState);
            if (!String.IsNullOrEmpty(record.NewState)) logEvent.Properties.Add(nameof(LogRecord.NewState), record.NewState);
            if (!String.IsNullOrEmpty(record.StackTrace)) logEvent.Properties.Add("nuviotStackTrace", record.StackTrace);

            logEvent.Properties.Add("nuviotApp", _appName);
            logEvent.Properties.Add("nuviotEnvironment", _environment);

            logEvent.Properties.Add("NuvIoTLogLevel", record.LogLevel);

            foreach (var prop in record.Parameters)
                logEvent.Properties.Add(prop.Key, prop.Value);


            _logger.Log(logEvent);

            _consoleLogWriter.WriteError(record);

            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            var logEvent = new LogEventInfo()
            {
                Message = record.Message,
                Level = LogLevel.Info
            };

            logEvent.Properties.Add("NuvIoTLogLevel", record.LogLevel);

            if (!String.IsNullOrEmpty(record.Tag))
            {
                if (!record.Tag.StartsWith("["))
                    record.Tag = "[" + record.Tag;

                if (!record.Tag.EndsWith("]"))
                    record.Tag += "]";

                logEvent.Properties.Add("nuviotTag", record.Tag);

                var parts = record.Tag.Split('_');
                if (parts.Length > 1)
                {
                    var className = parts[0].Substring(1);
                    logEvent.Properties.Add("nuviotClass", className);
                }
            }
            else
            {
                var regEx = new Regex(@"\[[\w+_<>]+\]");
                var match = regEx.Match(record.Message);
                if(match.Success)
                {
                    logEvent.Properties.Add("nuviotTag", match.Value);
                
                    var parts = match.Value.Split('_');
                    if(parts.Length > 1)
                    {
                        var className = parts[0].Substring(1);
                        logEvent.Properties.Add("nuviotClass", className);
                    }
                }
            }

            logEvent.Properties.Add("nuviotApp", _appName);
            logEvent.Properties.Add("nuviotEnvironment", _environment);

            if (!String.IsNullOrEmpty(record.HostId)) logEvent.Properties.Add(nameof(LogRecord.HostId), record.HostId);
            if (!String.IsNullOrEmpty(record.InstanceId)) logEvent.Properties.Add(nameof(LogRecord.InstanceId), record.InstanceId);
            if (!String.IsNullOrEmpty(record.Area)) logEvent.Properties.Add(nameof(LogRecord.Area), record.Area);
            if (!String.IsNullOrEmpty(record.Version)) 
                logEvent.Properties.Add("nuviotVersion", record.Version);
            else
                logEvent.Properties.Add("nuviotVersion", _version);

            if (!String.IsNullOrEmpty(record.OldState)) logEvent.Properties.Add(nameof(LogRecord.OldState), record.OldState);
            if (!String.IsNullOrEmpty(record.NewState)) logEvent.Properties.Add(nameof(LogRecord.NewState), record.NewState);
            

            foreach (var prop in record.Parameters)
            {
                logEvent.Properties.Add(prop.Key, prop.Value);
            }

            _logger.Log(logEvent);
            _consoleLogWriter.WriteEvent(record);

            return Task.CompletedTask;
        }
    }
}
