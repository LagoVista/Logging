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
    }
}
