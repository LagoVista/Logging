// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 25ab3c3b9f27760984ebeec5ce65cc89c2d388d3024dc2bdd01a7dfcea60b53d
// IndexVersion: 2
// --- END CODE INDEX META ---
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IInstrumentationKeyProvider
    {
        bool DebugMode { get; }
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
