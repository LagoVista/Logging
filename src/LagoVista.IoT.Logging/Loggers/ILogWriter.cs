using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    public class LoggerConstants
    {
        public const string CATCH_ALL_ID = "C69C298C896A4E30BFA5F0DED72D821C";
    }

    public interface ILogWriter
    {        
        Task WriteEvent(LogRecord record);
        Task WriteError(LogRecord record);
    }
}
