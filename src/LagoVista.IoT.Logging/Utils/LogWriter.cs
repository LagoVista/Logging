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
            try
            {
                var tag = record.Tag.Replace("[", "[Error_");
                Console.Error.Write($"{DateTime.Now.ToString("HH:mm.ss.fff")} {tag} - {record.Message?.TrimEnd('.')}");
                if (!String.IsNullOrEmpty(record.Details))
                    Console.Error.Write($"; DETAILS={record.Details.Trim()}");

                if (!String.IsNullOrEmpty(record.Message))
                    Console.Error.Write($"; MESSAGE={record.Message.Trim()}");

                if (!String.IsNullOrEmpty(record.StackTrace))
                    Console.Error.Write($"; STACK={record.StackTrace.Trim().Replace("\n", "\\n").Replace("\r", "\\r")}");

                Console.Error.WriteLine(";");
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR LOGGING FAILURE: " + ex.ToString());   
            }

            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            if(!String.IsNullOrEmpty(record.Area))
                Console.Write($"{DateTime.Now.ToString("HH:mm.ss.fff")} [{record.LogLevel}] - {record.Area} - {record.Message.TrimEnd('.')}");
            else if(!String.IsNullOrEmpty(record.Tag))
                Console.Write($"{DateTime.Now.ToString("HH:mm.ss.fff")} [{record.LogLevel}] - {record.Tag} - {record.Message.TrimEnd('.')}");
            else
                Console.Write($"{DateTime.Now.ToString("HH:mm.ss.fff")} [{record.LogLevel}] - {record.Message.TrimEnd('.')}");

            if (!String.IsNullOrEmpty(record.Details))
                Console.Write($"; DETAILS={record.Details}");

            foreach (var parameter in record.Parameters)
                Console.Write($"; {parameter.Key}={parameter.Value}");

            Console.WriteLine(";");

            return Task.CompletedTask;
        }
    }
}
