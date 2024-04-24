using LagoVista.IoT.Logging.Loggers;
using Microsoft.Extensions.Configuration;

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
            

            _writer = Startup.CreateLogZWriter(root);
            
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