// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 7bbb879ba93d90055082a43e6ce424088ea49af73e2edb21e47fc1b5eeb7f781
// IndexVersion: 0
// --- END CODE INDEX META ---
using LagoVista.IoT.Logging.Loggers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LagoVista.IoT.Logging.Tests
{
    public class Tests
    {
        ILogWriter _writer;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder();
            var root = configuration.Build();
            var section = root.GetSection("LogZIO");
            

            _writer = Startup.CreateLogZWriter(root,"1.1.1", "test", "test");
            
        }

        [Test]
        public void WriteToLog()
        {
            _writer.WriteEvent(new Models.LogRecord()
            {
                Tag = "[Hello World]",
                Message = "Hello World"
            });
        }

        [Test]
        public void WriteErrorToLog()
        {
            _writer.WriteError(new Models.LogRecord()
            {
                Tag = "[Hello World]",
                Message = "Hello World"
            });
        }
    }


}