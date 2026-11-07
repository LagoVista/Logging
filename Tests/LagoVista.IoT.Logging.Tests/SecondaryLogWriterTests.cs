using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Tests
{
    public class SecondaryLogWriterTests
    {
        [Test]
        public async Task SecondaryWriter_ReceivesEventsAndErrorsWithoutReplacingPrimary()
        {
            var primary = new RecordingWriter();
            var secondary = new RecordingWriter();
            var logger = new TestLogger(primary);
            logger.ConfigureSecondaryLogWriter(secondary);

            await logger.EventAsync("first");
            await logger.ErrorAsync("failed");

            Assert.That(primary.Events, Has.Count.EqualTo(1));
            Assert.That(primary.Errors, Has.Count.EqualTo(1));
            Assert.That(secondary.Events, Has.Count.EqualTo(1));
            Assert.That(secondary.Errors, Has.Count.EqualTo(1));
            Assert.That(secondary.Events[0].Message, Is.EqualTo("first"));

            logger.ConfigureSecondaryLogWriter(null);
            await logger.EventAsync("second");
            Assert.That(primary.Events, Has.Count.EqualTo(2));
            Assert.That(secondary.Events, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task SecondaryWriterFailure_DoesNotBreakPrimary()
        {
            var primary = new RecordingWriter();
            var secondary = new RecordingWriter { Fail = true };
            var logger = new TestLogger(primary);
            logger.ConfigureSecondaryLogWriter(secondary);

            Assert.DoesNotThrowAsync(async () => await logger.EventAsync("first"));
            Assert.DoesNotThrowAsync(async () => await logger.ErrorAsync("failed"));
            Assert.That(primary.Events, Has.Count.EqualTo(1));
            Assert.That(primary.Errors, Has.Count.EqualTo(1));
        }

        private sealed class TestLogger : LoggerBase
        {
            public TestLogger(ILogWriter primary) : base(primary) { }

            protected override void SetRecordIdentifiers(LogRecord record) { }

            public Task EventAsync(string message) =>
                InsertEventAsync(new LogRecord { LogLevel = "Event", Message = message });

            public Task ErrorAsync(string message) =>
                InsertErrorAsync(new LogRecord { LogLevel = "Error", Message = message });
        }

        private sealed class RecordingWriter : ILogWriter
        {
            public readonly List<LogRecord> Events = new List<LogRecord>();
            public readonly List<LogRecord> Errors = new List<LogRecord>();
            public bool Fail { get; set; }

            public Task WriteEvent(LogRecord record)
            {
                if (Fail) throw new InvalidOperationException("Secondary unavailable");
                Events.Add(record);
                return Task.CompletedTask;
            }

            public Task WriteError(LogRecord record)
            {
                if (Fail) throw new InvalidOperationException("Secondary unavailable");
                Errors.Add(record);
                return Task.CompletedTask;
            }
        }
    }
}
