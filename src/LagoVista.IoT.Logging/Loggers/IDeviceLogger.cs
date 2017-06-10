using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IDeviceLogger : ILogger
    {
        String HostId { get; }
        string InstanceId { get; }

        
        /* Log performance */
        void AddMetric(string deviceTypeId, string deviceId, string measure, double duration);

        void AddMetric(string deviceTypeId, string deviceId, string measure, TimeSpan duration);

        void AddMetric(string deviceTypeId, string deviceId, string measure, int count = 1);
        /* Log errors */

        void AddError(string deviceTypeId, string deviceId, string tag, string message, params KeyValuePair<string, string>[] args);

        void AddError(string deviceTypeId, string deviceId, ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string deviceTypeId, string deviceId, string configurationSetting, string error, params KeyValuePair<string, string>[] args);

    }
}