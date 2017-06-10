using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IInstrumentationKeyProvider
    {
        string AdminInstrumentationKey { get; set; }
        string HostInstrumentationKey { get; set; }
        string InstanceInstrumentationKey { get; set; }
        string DeviceInstrumentationKey { get; set; }
    }
}
