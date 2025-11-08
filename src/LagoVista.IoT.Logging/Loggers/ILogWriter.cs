// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 961a7e6b60f2f411b0df65afd363ae62d016aaea39c8043a0421ce6980200cde
// IndexVersion: 2
// --- END CODE INDEX META ---
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
