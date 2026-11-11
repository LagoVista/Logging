// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: d3d8f92cd024bd70004185b71abd1e4495249eb9e1402aaef6f01cc8ddef0e56
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LagoVista.IoT.Logging
{
    public class Startup
    {
        /// <summary>F218: proves consumption of this specific new Logging NuGet package.</summary>
        public static string F218LoggingPackageMarker() => "F218-Logging";

        /// <summary>
        /// Compatibility factory for older hosts. Logz.io has been retired: do not
        /// require a shipping token, configure NLog, or create a network writer.
        /// Host diagnostics are written to console during bootstrap and to the
        /// Cassandra diagnostic sink once the hosted bridge has started.
        /// </summary>
        public static ILogWriter CreateLogZWriter(IConfiguration configuration, string version, string app, string environment)
        {
            return new ConsoleLogWriter();
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
            // The host owns its bootstrap writer; do not reset global NLog targets here.
            services.AddScoped<IAzureLogReader, AzureLogReader>();
        }
    }
}
