using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IHostLogger
    {
        String HostInstrumentationKey { get; }

        String InstanceInstrumentationKey { get;  }

        String DeviceInstrumentationKey { get; }

        String HostId { get; }

        void AddCustomEvent(LagoVista.Core.PlatformSupport.LogLevel level, string tag, string customEvent, params KeyValuePair<string, string>[] args);

        void AddError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddException(string tag, Exception ex, params KeyValuePair<string, string>[] args);

        void AddMetric(string measure, double duration);

        void AddMetric(string measure, TimeSpan duration);

        void AddMetric(string measure, int count = 1);

        void Start();
        void Stop();
    }
}
