using SharpMason.Logging.DiskFile;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace SharpMason.Logging
{
    [ProviderAlias("Mason")]
    public class LoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string,Logger> _loggers = new();
        private LoggerOption _option;
        private readonly IDisposable? _onChangeToken;
        private readonly Processor _processor;


        public LoggerProvider(IOptionsMonitor<LoggerOption> options, ILoggerWriter loggerWriter, IFileWriter fileWriter)
        {
            _option = options.CurrentValue;
            _onChangeToken = options.OnChange(RefreshLogger);
            _processor = new Processor(_option.MaxQueueCount, loggerWriter, fileWriter);
        }

        /// <summary>
        /// 刷新日志配置
        /// </summary>
        /// <param name="loggerOption"></param>
        private void RefreshLogger(LoggerOption loggerOption)
        {
            _option = loggerOption;
            foreach (var logger in _loggers.Values)
            {
                //日志配置项
                logger.LoggerOption = loggerOption;
            }
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, _ => new Logger(_processor, categoryName, _option));
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
            _processor.Dispose();
        }
    }
}
