using LagoVista.Core;
using LagoVista.Core.Models;
using System;

namespace LagoVista.IoT.Logging.Models
{
    /// <summary>
    /// Lightweight diagnostic projection intended for humans and automation to scan
    /// before requesting the full error record by id.
    /// </summary>
    public class ApplicationErrorSummary
    {
        public NormalizedId32 Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Application { get; set; }
        public string Environment { get; set; }
        public string LogLevel { get; set; }
        public string Tag { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string Version { get; set; }
        public string HostId { get; set; }
    }
}
