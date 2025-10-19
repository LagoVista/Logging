// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 552f4a6ab6e21c8d615d154ee3f39c7bb1f128623c2b117ec4046abf61320c4f
// IndexVersion: 0
// --- END CODE INDEX META ---
using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IInstanceLogger : ILogger
    {
        String DeviceInstrumentationKey { get; }

        String HostId { get; }

        String InstanceId { get;  }

     
        void AddError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string configurationSetting, string error, params KeyValuePair<string, string>[] args);

     
        void AddMetric(string measure, double duration);

        void AddMetric(string measure, TimeSpan duration);

        void AddMetric(string measure, int count = 1);
        
        /* Log start/stop */
        void Start();
        void Stop();
    }
}
