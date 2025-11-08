// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 5a58bcfbd84a04e8ab17c21f40ab88512e83ff2b4ef85f52d590b959f475c59b
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IHostLogger : ILogger
    {
        String InstrumentationKey { get; }

        String InstanceInstrumentationKey { get;  }

        String DeviceInstrumentationKey { get; }

        String HostId { get; }

   
        void AddError(string tag, string message, params KeyValuePair<string, string>[] args);
        void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string configurationSetting, string error, params KeyValuePair<string, string>[] args);
        
        void AddMetric(string measure, double duration);

        void AddMetric(string measure, TimeSpan duration);

        void AddMetric(string measure, int count = 1);

        void Start();
        void Stop();
    }
}
