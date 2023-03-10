using Microsoft.Extensions.Logging;

namespace SharpMason.Logging.AspNetCore
{
    internal static class TraceLogExtensions
    {
        private static readonly EventId TraceEventId = new(-110, "Trace");

        internal static void LogTrace(this ILogger logger, LogLevel logLevel, TraceEntry state, Exception? exception=null)
        {
            logger.Log(logLevel, TraceEventId, state, exception, (m, e) => string.Empty);
        }
    }
}
