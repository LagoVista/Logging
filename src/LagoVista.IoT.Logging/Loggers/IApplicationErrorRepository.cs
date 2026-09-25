using LagoVista.IoT.Logging.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    /// <summary>
    /// Durable, bounded repository for application errors and exceptions.
    /// Implementations are expected to treat these records as diagnostic scratch data,
    /// not authoritative application history.
    /// </summary>
    public interface IApplicationErrorRepository
    {
        Task WriteErrorAsync(LogRecord record, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ApplicationErrorSummary>> GetRecentErrorsAsync(
            int take = 100,
            string application = null,
            string environment = null,
            CancellationToken cancellationToken = default);

        Task<LogRecord> GetErrorAsync(string id, CancellationToken cancellationToken = default);
    }
}
