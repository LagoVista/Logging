using LagoVista.Core.Models.UIMetaData;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Utils
{
    public class LogZReader : ILogReader
    {
        public Task<ListResponse<LogRecord>> GetAllErrorsAsync(ListRequest listRequest)
        {
            return Task<ListResponse<LogRecord>>.FromResult(new ListResponse<LogRecord>());
        }

        public Task<ListResponse<LogRecord>> GetErrorsAsync(ResourceType resourceType, ListRequest listRequest)
        {
            return Task<ListResponse<LogRecord>>.FromResult(new ListResponse<LogRecord>());
        }

        public Task<ListResponse<LogRecord>> GetErrorsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any)
        {
            return Task<ListResponse<LogRecord>>.FromResult(new ListResponse<LogRecord>());
        }

        public Task<ListResponse<LogRecord>> GetLogRecordsAsync(ResourceType resourceType, ListRequest listRequest)
        {
            return Task<ListResponse<LogRecord>>.FromResult(new ListResponse<LogRecord>());
        }

        public Task<ListResponse<LogRecord>> GetLogRecordsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any)
        {
            return Task<ListResponse<LogRecord>>.FromResult(new ListResponse<LogRecord>());
        }
    }
}
