using Masonlog.Enhancer;
using Microsoft.Extensions.Logging;
using SharpMason.Logging.Utils;

namespace SharpMason.Logging
{
    public class Logger : ILogger
    {
        private readonly Processor _processor;
        /// <summary>
        /// 日志名
        /// </summary>
        internal string CategoryName { get; set; }

        /// <summary>
        /// 日志配置
        /// </summary>
        internal LoggerOption LoggerOption { get; set; }
        /// <summary>
        /// 主机地址
        /// </summary>
        private readonly string HostIP;

        public Logger(Processor processor,string categoryName, LoggerOption loggerOption)
        {
            CategoryName = categoryName;
            LoggerOption = loggerOption;
            HostIP = NetWorkUtil.GetHostIp();
            _processor = processor;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None) return false;

            foreach (var prefix in GetKeyPrefix(CategoryName))
            {
                if (LoggerOption.LogLevels.TryGetValue(prefix, out var level))
                {
                    return logLevel >= level;
                }
            }
            return false;
        }

        /// <summary>
        /// 日志前缀
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<string> GetKeyPrefix(string name)
        {
            while (!string.IsNullOrEmpty(name))
            {
                yield return name;
                var lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }
                name = name[..lastIndexOfDot];
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var log = state as LogEntry;
            log ??= new LogEntry
            {
                EventId = eventId.Id,
                Exception = exception?.ToString(),
                Message = state?.ToString()
            };
            // 附加信息
            log.SpanId = LogContextAccessor.Current?.TryGetValue<string>(LogConst.SpanId);
            log.TraceId = LogContextAccessor.Current?.TryGetValue<string>(LogConst.TraceId);
            log.ParentSpanId = LogContextAccessor.Current?.TryGetValue<string>(LogConst.ParentSpanId);
            log.HostIp = HostIP;
            log.LogLevel = StrLogLevel(logLevel);
            log.RecordTime = DateTime.Now;
            if (LoggerOption.Console)
            {
                ConsoleColorPrint(logLevel, log);
            }
            _processor.Enqueue(log);
        }

        /// <summary>
        /// 日志基本字符描述
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        private static string StrLogLevel(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "trce",
                LogLevel.Debug => "dbug",
                LogLevel.Information => "info",
                LogLevel.Warning => "warn",
                LogLevel.Error => "fail",
                LogLevel.Critical => "crit",
                LogLevel.None => "none",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// 控制台打印日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ConsoleColorPrint(LogLevel logLevel, LogEntry message)
        {

            var color = ConsoleColor.DarkGreen;
            switch (logLevel)
            {
                case LogLevel.Trace:
                    break;
                case LogLevel.Debug:
                    break;
                case LogLevel.Information:
                    break;
                case LogLevel.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    color = ConsoleColor.Red;
                    break;
                case LogLevel.Critical:
                    color = ConsoleColor.Red;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("===================================================================================");
            Console.ForegroundColor = color;
            Console.WriteLine(message.ToIndentedJson());
        }
    }
}
