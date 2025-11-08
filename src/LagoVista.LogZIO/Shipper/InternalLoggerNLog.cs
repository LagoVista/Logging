// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: ad6359ce2a02db166037c4f87130d7be8a89ffb63cb8030284177831f2f7a8d7
// IndexVersion: 2
// --- END CODE INDEX META ---
using System;
using Logzio.DotNet.Core.InternalLogger;
using Logzio.DotNet.Core.Shipping;
using InternalLogger = NLog.Common.InternalLogger;

namespace Logzio.DotNet.NLog
{
    internal class InternalLoggerNLog : IInternalLogger
    {
        private readonly ShipperOptions _shipperOptions;
        private readonly IInternalLogger _internalLogger;

        public InternalLoggerNLog(ShipperOptions shipperOptions, IInternalLogger internalLogger)
        {
            _shipperOptions = shipperOptions;
            _internalLogger = internalLogger;
        }

        public void Log(Exception ex, string message, params object[] args)
        {
            if (ex != null)
                InternalLogger.Warn(ex, message, args);
            else
                InternalLogger.Trace(message, args);

            if (_shipperOptions.Debug)
                _internalLogger?.Log(ex, message, args);
        }
    }
}
