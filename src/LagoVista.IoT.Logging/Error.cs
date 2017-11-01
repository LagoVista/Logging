using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class Error
    {
        public string InstanceHostId { get; set; }
        public string InstanceId { get; set; }
        public string DeploymentId { get; set; }
        public string DeviceConfigurationId { get; set; }

        public string ConfigurationType { get; set; }
        public string ConfigurationId { get; set; }
        public string DeviceId { get; set; }

        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public List<KeyValuePair<string, string>> Extras { get; set; }


    }
}
