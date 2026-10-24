using System;
using System.Collections.Generic;
using LagoVista.IoT.Logging.Models;
using NUnit.Framework;

namespace LagoVista.IoT.Logging.Tests
{
    public class LogRecordUnitTests
    {
        [Test]
        public void NewRecordInitializesIdentifiersAndParameters()
        {
            var record = new LogRecord();
            Assert.That(record.Id.ToString(), Is.Not.Empty);
            Assert.That(record.EscapeCRLF, Is.True);
            Assert.That(record.Parameters, Is.Not.Null.And.Empty);
        }

        [Test]
        public void AddKVPsRoutesKnownFieldsAndRetainsCustomParameters()
        {
            var record = new LogRecord();
            record.AddKVPs(new[]
            {
                new KeyValuePair<string, string>("deviceid", "device-123"),
                new KeyValuePair<string, string>("custom", "value")
            });
            Assert.That(record.DeviceId, Is.EqualTo("device-123"));
            Assert.That(record.Parameters["custom"], Is.EqualTo("value"));
            Assert.That(record.Details, Does.Contain("custom"));
        }
    }
}
