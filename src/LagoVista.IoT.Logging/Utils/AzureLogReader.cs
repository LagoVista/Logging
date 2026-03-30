using LagoVista.Core.Models.UIMetaData;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Utils
{
    internal class AzureLogReader : IAzureLogReader
    {
        // This is a placeholder implementation. The actual implementation would involve querying Azure Log Analytics or Application Insights.
        // this may have been replaced by LogzIo

        public Task<ListResponse<LogRecord>> GetAllErrorsAsync(ListRequest listRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<LogRecord>> GetErrorsAsync(ResourceType resourceType, ListRequest listRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<LogRecord>> GetErrorsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<LogRecord>> GetLogRecordsAsync(ResourceType resourceType, ListRequest listRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<LogRecord>> GetLogRecordsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any)
        {
            throw new NotImplementedException();
        }
    }
}
