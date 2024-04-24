using LagoVista.Core.Interfaces;
using LagoVista.IoT.Logging.Loggers;
using Logzio.DotNet.NLog;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Config;
using System;

namespace LagoVista.IoT.Logging
{
    public class Startup
    {
        public static ILogWriter CreateLogZWriter(IConfigurationRoot configurationRoot)
        {
            var section = configurationRoot.GetSection("LogzIO");
            var token = "lDClQlWGOpREionETTlFfJsqswNbNysd";// section["Token"];
            var environment = section["Environment"];

            var config = new LoggingConfiguration();

            var target = new LogzioTarget()
            {
                Name = "Logzio",
                Token = token,
                LogzioType = "nlog",
                ListenerUrl = "https://listener.logz.io:8071",
                BufferSize = 100,
                BufferTimeout = TimeSpan.Parse("00:00:05"),
                RetriesMaxAttempts = 3,
                RetriesInterval = TimeSpan.Parse("00:00:02"),
                Debug = true,
                JsonKeysCamelCase = false,
                AddTraceContext = false,
            };            

            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, target);
            
            LogManager.Configuration = config;

            return new Utils.LogZWriter();
        }

        public static ILogReader CreateLogZReader(IConfigurationRoot configurationRoot)
        {
            return new Utils.LogZReader();
        }
    }
}
