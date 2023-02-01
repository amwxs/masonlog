namespace SharpMason.Logging
{
    public class LogEntry
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// 当前SpanId
        /// </summary>
        public string? SpanId { get; set; }

        /// <summary>
        /// 父级SpanId
        /// </summary>
        public string? ParentSpanId { get; set; }

        /// <summary>
        /// 链路ID
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// 主机IP
        /// </summary>
        public string HostIP { get; set; }
        /// <summary>
        /// 事件Id
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string? Exception { get; set; }
        /// <summary>
        /// 日志消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
