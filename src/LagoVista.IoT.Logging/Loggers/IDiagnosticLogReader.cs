using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    /// <summary>
    /// A bounded diagnostic query. Application and environment are required because
    /// diagnostic events are partitioned by those values in the storage layer.
    /// </summary>
    public sealed class DiagnosticLogQuery
    {
        public string Application { get; set; }
        public string Environment { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public string Level { get; set; }
        public string Tag { get; set; }
        public int PageSize { get; set; } = 100;
        public string ContinuationToken { get; set; }
    }

    public sealed class DiagnosticLogPage
    {
        public IReadOnlyList<LogRecord> Records { get; set; }
        public string ContinuationToken { get; set; }
    }

    /// <summary>
    /// Provider-independent, read-only diagnostics access. Authorization belongs to
    /// the API/MCP caller; implementations must still require bounded queries.
    /// </summary>
    public interface IDiagnosticLogReader
    {
        Task<DiagnosticLogPage> QueryAsync(DiagnosticLogQuery query, CancellationToken cancellationToken = default);
    }
}
