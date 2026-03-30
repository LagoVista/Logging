// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: d3d8f92cd024bd70004185b71abd1e4495249eb9e1402aaef6f01cc8ddef0e56
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Utils;
using Logzio.DotNet.NLog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using System;

namespace LagoVista.IoT.Logging
{
    public class Startup
    {
        public static ILogWriter CreateLogZWriter(IConfiguration configuration, string version, string app, string environment)
        {
            var section = configuration.GetSection("LogzIO");
            var token = section.Require("AccessToken");

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

            return new Utils.LogZWriter(version, app, environment);
        }

        public static ILogReader CreateLogZReader(IConfigurationRoot configurationRoot)
        {
            return new Utils.LogZReader();
        }

    }
}


namespace LagoVista.DependencyInjection
{
    public static class LogoVistaLoggingModule
    {
        public static void AddLagoVistaLoggingModule(this IServiceCollection services, IConfigurationRoot configRoot, IAdminLogger logger)
        {
            services.AddScoped<IAzureLogReader, AzureLogReader>();
            LagoVista.IoT.Logging.Startup.CreateLogZWriter(configRoot, "1.0.0", "LagoVista.IoT.Logging", "Production");
        }
    }
}
