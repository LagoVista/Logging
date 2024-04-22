using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Utils
{
    public class ConsoleLogWriter : ILogWriter
    {
        public Task WriteError(LogRecord record)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"[error] {DateTime.Now.ToShortTimeString()} {record.Area} - {record.Message}");
            if (!String.IsNullOrEmpty(record.Details))
                Console.WriteLine(record.Details);

            Console.ResetColor();

            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            Console.WriteLine($"[event] {DateTime.Now.ToShortTimeString()} {record.Area} - {record.Message}");
            if (!String.IsNullOrEmpty(record.Details))
                Console.WriteLine(record.Details);            

            return Task.CompletedTask;
        }
    }
}
