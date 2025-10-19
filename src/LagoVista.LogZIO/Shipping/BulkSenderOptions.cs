// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: da3faf8832aa913d0bd06546c2118a4aaa946a796ac0df545bce0e733fa0a689
// IndexVersion: 0
// --- END CODE INDEX META ---
using System;
using System.IO;

namespace Logzio.DotNet.Core.Shipping
{
    public class BulkSenderOptions
    {
        public string Token { get; set; } = "INVALID TOKEN";
        public string ListenerUrl { get; set; } = "https://listener.logz.io:8071";
        public TimeSpan RetriesInterval { get; set; } = TimeSpan.FromSeconds(2);
        public int RetriesMaxAttempts { get; set; } = 3;
        public string Type { get; set; } = "dotnet";
        public bool Debug { get; set; }
        public string DebugLogFile { get; set; } = String.Empty;
        public bool UseGzip { get; set; } = false;
        public string ProxyAddress { get; set; } = String.Empty;
        public bool ParseJsonMessage { get; set; } = false;
        public bool JsonKeysCamelCase { get; set; } = false;
        public bool AddTraceContext { get; set; } = false;
        public bool UseStaticHttpClient { get; set; } = false;
        
    }
}