// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: ce6a15c7f362c5805b0880f05e551a794b48b9bf15607db5eb5274aca9147c70
// IndexVersion: 0
// --- END CODE INDEX META ---
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
