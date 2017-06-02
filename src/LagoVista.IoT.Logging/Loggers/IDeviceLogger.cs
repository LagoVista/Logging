using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IDeviceLogger
    {
        String InstrumentationKey { get; }

        void AddEvent(LagoVista.Core.PlatformSupport.LogLevel level, string tag, string instanceId, string hostId, string deviceTypeId, string deviceId, string customEvent, params KeyValuePair<string, string>[] args);
        /* Log performance */
        void AddMetric(string instanceId, string hostId, string deviceTypeId, string deviceId, string measure, double duration);

        void AddMetric(string instanceId, string hostId, string deviceTypeId, string deviceId, string measure, int count);
        /* Log errors */

        void AddError(string instanceId, string hostId, string deviceTypeId, string deviceId, string tag, string message, params KeyValuePair<string, string>[] args);

        void AddError(string instanceId, string hostId, string deviceTypeId, string deviceId, ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string instanceId, string hostId, string deviceTypeId, string deviceId, string tag, string message, params KeyValuePair<string, string>[] args);

        void AddException(string instanceId, string hostId, string deviceTypeId, string deviceId, string tag, Exception ex, params KeyValuePair<string, string>[] args);

    }
}