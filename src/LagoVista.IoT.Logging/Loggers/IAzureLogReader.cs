using LagoVista.Core.Models.UIMetaData;
using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IAzureLogReader
    {
        Task<ListResponse<LogRecord>> GetLogRecordsAsync(ResourceType resourceType, ListRequest listRequest);
        Task<ListResponse<LogRecord>> GetErrorsAsync(ResourceType resourceType, ListRequest listRequest);
        Task<ListResponse<LogRecord>> GetLogRecordsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any);
        Task<ListResponse<LogRecord>> GetErrorsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any);
        Task<ListResponse<LogRecord>> GetAllErrorsAsync(ListRequest listRequest);

    }
}
