// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: def31dc628538b10489a477d3ff3b9cb8f16a6c531c5f68068314444779b67a5
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.Core.Models.UIMetaData;
using LagoVista.IoT.Logging.Models;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
	public enum ResourceType
	{
		WebTools,
		API,
		Host,
		Instance,
		PipelineModule,
		Device,
		Any,
		Scheduler,
	}

	public interface ILogReader
	{
		Task<ListResponse<LogRecord>> GetLogRecordsAsync(ResourceType resourceType, ListRequest listRequest);
		Task<ListResponse<LogRecord>> GetErrorsAsync(ResourceType resourceType, ListRequest listRequest);
		Task<ListResponse<LogRecord>> GetLogRecordsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any);
		Task<ListResponse<LogRecord>> GetErrorsAsync(string resourceId, ListRequest listRequest, ResourceType resourceType = ResourceType.Any);
		Task<ListResponse<LogRecord>> GetAllErrorsAsync(ListRequest listRequest);
	}
}