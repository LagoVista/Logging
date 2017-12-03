using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class Error
    {
        [JsonProperty("hostId", NullValueHandling = NullValueHandling.Ignore)]
        public string HostId { get; set; }

        [JsonProperty("instanceId", NullValueHandling = NullValueHandling.Ignore)]
        public string InstanceId { get; set; }

        [JsonProperty("configurationType", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationType { get; set; }

        [JsonProperty("configurationId", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationId { get; set; }


        [JsonProperty("solutionId", NullValueHandling = NullValueHandling.Ignore)]
        public string SolutionId { get; set; }
        [JsonProperty("deviceConfigurationId", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceConfigurationId { get; set; }

        [JsonProperty("deviceId", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceId { get; set; }
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public string Details { get; set; }
        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public List<KeyValuePair<string, string>> Extras { get; set; }

        public void SetEmptyValueToNull()
        {
            if (Extras != null && !Extras.Any()) Extras = null;
        }
    }
}
