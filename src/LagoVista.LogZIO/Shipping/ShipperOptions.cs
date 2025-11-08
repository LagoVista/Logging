// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: e9bb1cdcfa455e9eafb2a5091aa951a944aa7619bfab7ef2e05fc21c6f617b15
// IndexVersion: 2
// --- END CODE INDEX META ---
using System;

namespace Logzio.DotNet.Core.Shipping
{
    public class ShipperOptions
    {
        public ShipperOptions()
        {
            BufferSize = 100;
            BufferTimeLimit = TimeSpan.FromSeconds(5);
            BulkSenderOptions = new BulkSenderOptions();
        }

        public int BufferSize { get; set; }
        public TimeSpan BufferTimeLimit { get; set; }
        public bool Debug { get; set; }
        public string DebugLogFile { get; set; }
        public BulkSenderOptions BulkSenderOptions { get; set; }
    }
}