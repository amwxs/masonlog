namespace MasonPlatform.Models.Dtos
{
    public class LogEntryReq: PagerQuery
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// 链路ID
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// 事件Id
        /// </summary>
        public int? EventId { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string? LogLevel { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string? Exception { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 时间范围-开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 时间范围-结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 耗费时间大于
        /// </summary>

        public int? LT { get; set; }
        /// <summary>
        /// 耗费时间大于
        /// </summary>

        public int? GT { get; set; }
    }
}
