using LagoVista.IoT.Logging.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LagoVista.IoT.Logging.Tests
{
    [TestFixture]
    public class BootstrapWriterTests
    {
        [Test]
        public void LegacyBootstrapWriterDoesNotRequireLogzioToken()
        {
            var configuration = new ConfigurationBuilder().Build();

            var writer = Startup.CreateLogZWriter(configuration, "1.0.0", "api", "test");

            Assert.That(writer, Is.InstanceOf<ConsoleLogWriter>());
        }
    }
}
