// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 37be3665dd6bad80e14858ac37b87ba158e3e16392c76067a5fdffcdd30f4d25
// IndexVersion: 2
// --- END CODE INDEX META ---
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

            Console.WriteLine($"[ERROR] {record.Tag} - {DateTime.Now.ToShortTimeString()} {record.Area} - {record.Message}");
            if (!String.IsNullOrEmpty(record.Details))
                Console.WriteLine(record.Details);

            if (!String.IsNullOrEmpty(record.StackTrace))
                Console.WriteLine(record.StackTrace);

            Console.ResetColor();

            Console.Error.WriteLine($"[ERROR] {record.Tag} - {DateTime.Now.ToShortTimeString()} {record.Area} - {record.Message}");
            if (!String.IsNullOrEmpty(record.Details))
                Console.Error.WriteLine(record.Details);

            if (!String.IsNullOrEmpty(record.Message))
                Console.Error.WriteLine(record.Message);

            if (!String.IsNullOrEmpty(record.StackTrace))
                Console.Error.WriteLine(record.StackTrace);
            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {record.Area} - {record.Message}");

            if (!String.IsNullOrEmpty(record.Details))
                Console.WriteLine($"\t{record.Details}");

            foreach (var parameter in record.Parameters)
                Console.WriteLine($"\t{parameter.Key}={parameter.Value}");

            return Task.CompletedTask;
        }
    }
}
