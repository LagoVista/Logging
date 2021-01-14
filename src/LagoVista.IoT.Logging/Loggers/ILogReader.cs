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