using Microsoft.Extensions.Logging;

namespace SharpMason.Logging
{
    public class LoggerOption
    {
        public string? AppId { get; set; } = string.Empty;
        /// <summary>
        /// 是否前台打印
        /// </summary>
        public bool Console { get; set; }


        public string LocalFilePath { get; set; } = "/var/logs/";
        /// <summary>
        /// 日志级别
        /// </summary>
        public IDictionary<string, LogLevel> LogLevel { get; set; } = new Dictionary<string, LogLevel>();
        /// <summary>
        /// 日志队列长度
        /// </summary>
        public int MaxQueueCount { get; internal set; } = 5000;
    }
}
