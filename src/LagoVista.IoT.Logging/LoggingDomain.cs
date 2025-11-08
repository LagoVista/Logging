// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: e71ea6bf59f07f8633a0fe28796fdfe7e2ee9b5e2837b50d463229d061f76db0
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.Core.Attributes;
using LagoVista.Core.Models.UIMetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging
{

    [DomainDescriptor]
    public class LoggingDomain
    {
        public const string Logging = "Logging";
        
        [DomainDescription(Logging)]
        public static DomainDescription LoggingDescription
        {
            get
            {
                return new DomainDescription()
                {
                    Description = "Log Data.",
                    DomainType = DomainDescription.DomainTypes.BusinessObject,
                    Name = "Logging",
                    CurrentVersion = new Core.Models.VersionInfo()
                    {
                        Major = 0,
                        Minor = 8,
                        Build = 001,
                        DateStamp = new DateTime(2016, 12, 20),
                        Revision = 1,
                        ReleaseNotes = "Initial unstable preview"
                    }
                };
            }
        }
    }
}
