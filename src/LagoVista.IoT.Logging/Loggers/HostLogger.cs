using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class HostLogger : LoggerBase, IHostLogger
    {
        public HostLogger(ILogWriter writer) : base(writer)
        {

        }

        public void Init(string hostId)
        {
            HostId = hostId;
        }

        public string InstrumentationKey => throw new NotImplementedException();

        public string InstanceInstrumentationKey => throw new NotImplementedException();

        public string DeviceInstrumentationKey => throw new NotImplementedException();

        public string HostId { get; private set; }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
        }

        public void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            throw new NotImplementedException();
        }
    }
}
