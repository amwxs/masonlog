using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpMason.Extensions;

namespace MasonPlatform
{
    public class MasonExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<MasonExceptionFilter> _logger;
        private static string Message = "System Error Please Contact Admin";
        public MasonExceptionFilter(ILogger<MasonExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            context.Result = new ObjectResult(Pack.Fail(PackConst.ExceptionCode, Message));
            context.ExceptionHandled = true;
        }
    }
}
