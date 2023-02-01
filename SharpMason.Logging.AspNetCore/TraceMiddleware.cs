using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharpMason.Extensions.Context;
using SharpMason.Extensions.Utils;
using System.Diagnostics;
using System.Text;

namespace SharpMason.Logging.AspNetCore
{
    /// <summary>
    /// 追踪中间件
    /// </summary>
    public  class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private static readonly List<string> DefaultContents = new() {
         "image",
         "video",
         "audio",
         "excel",
         "world",
         "stream"
        };

        public TraceMiddleware(ILogger<TraceMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //设置日志上下文
            var logContext = BuildContext(httpContext);
            using var disposable = ContextAccessor.Build(logContext);

            //读取请求体
            var requestBody = await ReadRequestBodyAsync(httpContext.Request);
            //响应流
            using var currentStream = new MemoryStream();
            var originalBody = httpContext.Response.Body;
            httpContext.Response.Body = currentStream;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(httpContext);
            stopwatch.Stop();
            //读取响应内容
            var responseBody = await ReadResponseBodyAsync(httpContext.Response);

            //请求记录
            var req = new Request
            {
                Path = $"{httpContext.Request.Path}{httpContext.Request.QueryString.Value}",
                Method = httpContext.Request.Method,
                Body = requestBody,
            };
            //请求头
            foreach (var item in httpContext.Request.Headers)
            {
                req.Headers.Add(item.Key, item.Value.ToString());
            }
            //响应记录
            var res = new Response
            {
                Body = responseBody,
                StatusCode = httpContext.Response.StatusCode,
            };
            //响应头
            foreach (var item in httpContext.Response.Headers)
            {
                res.Headers.Add(item.Key, item.Value.ToString());
            }

            _logger.LogTrace(LogLevel.Information, new TraceEntry { Request = req, Response = res,TimeSpan= stopwatch.Elapsed.Milliseconds });
            await currentStream.CopyToAsync(originalBody);
        }

        /// <summary>
        /// 构建logContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Context BuildContext(HttpContext context)
        {
            context.Request.Headers.TryGetValue(LogConst.TraceId, out var traceId);
            if (string.IsNullOrEmpty(traceId))
            {
                traceId = GuidUtil.NewGuid();
            }
            var pSpanId = string.Empty;
            context.Request.Headers.TryGetValue(LogConst.SpanId, out var spanId);
            if (string.IsNullOrEmpty(spanId))
            {
                spanId = GuidUtil.NewGuid();
            }
            else
            {
                pSpanId = spanId.ToString();
                spanId = GuidUtil.NewGuid();
            }
            var logContext = new Context();
            logContext.TryAdd(LogConst.TraceId, traceId.ToString());
            logContext.TryAdd(LogConst.SpanId, spanId.ToString());
            logContext.TryAdd(LogConst.ParentSpanId, pSpanId);
            return logContext;
        }
        /// <summary>
        /// 读取请求体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            try
            {
                var requestBody = string.Empty;
                if (request.ContentType != null && request.ContentType.ToLower().Contains("multipart/form-data"))
                    return "文件详情忽略";

                if (request.ContentLength is not > 0) return requestBody;
                
                request.EnableBuffering();

                //最多读取2048字节
                var length = 2048;
                if (request.ContentLength < length)
                    length = (int)request.ContentLength.Value;
                var readBuffer = new char[length];

                using var reader = new StreamReader(request.Body, Encoding.UTF8, false, length, true);
                var readLength = await reader.ReadAsync(readBuffer, 0, length);

                if (readLength > 0)
                    requestBody = new string(readBuffer, 0, readLength);

                //重置
                request.Body.Position = 0;
                return requestBody;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Request Body 错误");
                throw;
            }
        }
        /// <summary>
        /// 读取响应体
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            if (response.ContentType != null)
            {
                if (DefaultContents.Any(item => response.ContentType.Contains(item)))
                {
                    return "检查到文件流，内容不记录";
                }
            }

            try
            {
      
                var responseStr = string.Empty;
          
                var length = 2048;
                if (response.Body.Length < length)
                    length = (int)response.Body.Length;

                //读取指定长度
                response.Body.Position = 0;
                var readBuffer = new char[length];
                using var reader = new StreamReader(response.Body, Encoding.UTF8, false, length, true);
                var readLength = await reader.ReadAsync(readBuffer, 0, length);

                if (readLength > 0)
                    responseStr = new string(readBuffer, 0, readLength);
                //重置
                response.Body.Position = 0;
                return responseStr;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Response Body 错误");
                throw;
            }
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogTrace(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceMiddleware>();
        }
    }
}