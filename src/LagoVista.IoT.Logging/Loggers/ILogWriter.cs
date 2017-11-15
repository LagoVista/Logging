using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface ILogWriter
    {
        Task WriteEvent(LogRecord record);
        Task WriteError(LogRecord record);
    }
}
