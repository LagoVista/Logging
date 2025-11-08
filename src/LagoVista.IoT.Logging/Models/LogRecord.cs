// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: f88f2af585f54e23bdbca71e8ef34e632202c29006e12b7cc7dfc7945c85453d
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.Core;
using LagoVista.Core.Attributes;
using LagoVista.Core.Interfaces;
using LagoVista.Core.Models;
using LagoVista.IoT.Logging.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Models
{
    [EntityDescription(LoggingDomain.Logging, LoggingResources.Names.LogRecords_Title, LoggingResources.Names.LogRecord_Description,
       LoggingResources.Names.LogRecord_Description, EntityDescriptionAttribute.EntityTypes.SimpleModel, typeof(LoggingResources),
        ListUIUrl: "/sysadmin/areas/logs", Icon: "icon-ae-coding-laptop")]
    public class LogRecord : IIDEntity
    {
        public LogRecord()
        {

            Id = Guid.NewGuid().ToId();
            TimeStamp = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string LogLevel { get; set; }
        public string HostId { get; set; }
        public string Version { get; set; }
        public string PemId { get; set; }
        public string InstanceId { get; set; }
        public string PipelineModuleId { get; set; }
        public string DeviceTypeId { get; set; }
        public string DeviceId { get; set; }
        public string ActivityId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ErrorCode { get; set; }
        public string Area { get; set; }
        public string Tag { get; set; }
        public double Measure { get; set; }
        public double MS { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public string StackTrace { get; set; }
        public string OldState { get; set; }
        public string NewState { get; set; }

        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        public void AddKVPs(KeyValuePair<string, string>[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (Parameters.ContainsKey(arg.Key))
                    {
                        var dupKeyName = $"-dup-{Guid.NewGuid().ToId()}";
                        Parameters.Add(dupKeyName, arg.Value);
                    }
                    else
                    {
                        switch (arg.Key.ToLower())
                        {
                            case "pemid": PemId = arg.Value; break;
                            case "activityid": ActivityId = arg.Value; break;
                            case "deviceid": DeviceId = arg.Value; break;
                            case "devicetypeid": DeviceTypeId = arg.Value; break;
                            case "hostid": HostId = arg.Value; break;
                            case "instanceid": InstanceId = arg.Value; break;
                            case "pipelinemoduleid": PipelineModuleId = arg.Value; break;
                            case "oldstate": OldState = arg.Value; break;
                            case "newstate": NewState = arg.Value; break;
                            case "message": Message = arg.Value; break;
                            case "version": Version = arg.Value; break;
                            default: Parameters.Add(arg.Key, arg.Value); break;
                        }
                    }
                }

                Details = JsonConvert.SerializeObject(Parameters);
            }
        }
    }
}
