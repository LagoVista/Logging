﻿using LagoVista.Core.Interfaces;
using LagoVista.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Models
{
    public class LogRecord: IIDEntity
    {
        public LogRecord()
        {
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

        public void AddKVPs(KeyValuePair<string, string>[] args)
        {
            if (args != null && args.Length > 0)
            {
                var dict = new Dictionary<string, string>();

                foreach (var arg in args)
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
                        default: dict.Add(arg.Key, arg.Value); break;
                    }
                }

                Details = JsonConvert.SerializeObject(dict);
            }
        }
    }
}
