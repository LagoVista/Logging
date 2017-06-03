using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IInstanceLogger
    {
        String InstanceInstrumentationKey { get;  }

        String DeviceInstrumentationKey { get; }

        String HostId { get; }

        String InstanceId { get;  }

        void AddCustomEvent(LagoVista.Core.PlatformSupport.LogLevel level, string tag, string customEvent, params KeyValuePair<string, string>[] args);

        void AddError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string configurationSetting, string error, params KeyValuePair<string, string>[] args);

        void AddException(string tag, Exception ex, params KeyValuePair<string, string>[] args);

        void AddMetric(string measure, double duration);

        void AddMetric(string measure, TimeSpan duration);

        void AddMetric(string measure, int count = 1);
        
        /* Log start/stop */
        void Start();
        void Stop();
    }
}
