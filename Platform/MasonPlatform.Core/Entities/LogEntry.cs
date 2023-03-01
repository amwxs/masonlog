namespace MasonPlatform.Models.Entities
{
    public class LogEntry
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// 当前SpanId
        /// </summary>
        public string? SpanId;

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
        /// </summary>PalatmentMason
        public string? HostIp { get; set; }
        /// <summary>
        /// 事件Id
        /// </summary>
        public int EventId { get; set; }
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
        /// 日志时间
        /// </summary>
        public DateTime RecordTime { get; set; }
       
        /// <summary>
        /// 请求内容
        /// </summary>

        public Request? Request { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>

        public Response? Response { get; set; }
        /// <summary>
        /// 耗费时间 毫秒
        /// </summary>

        public int TimeSpan { get; set; }
    }


    public class Request
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 请求方法 GET/POST/PUT/DELETE
        /// </summary>
        public string? Method { get; set; }
        /// <summary>
        /// 请求体
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// 头信息
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new();
    }

    public class Response
    {
        /// <summary>
        /// 响应内容
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// HTTP 状态码
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 头信息
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new();
    }
}
