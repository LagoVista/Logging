using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IInstrumentationKeyProvider
    {
        string AdminInstrumentationKey { get; }
        string HostInstrumentationKey { get; }
        string InstanceInstrumentationKey { get; }
        string DeviceInstrumentationKey { get; }

        String AdminAppId { get; }
        String HostAppId { get; }
        String InstanceAppId { get; }
        String DeviceAppId { get; }

        String AdminAPIKey { get; }

        String HostAPIKey { get; }
        String InstanceAPIKey { get; }
        String DeviceAPIKey { get; }
    }
}
