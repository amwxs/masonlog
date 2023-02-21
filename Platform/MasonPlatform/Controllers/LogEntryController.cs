using MasonPlatform.Models.Dtos;
using MasonPlatform.Models.Entities;
using MasonPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using SharpMason.Extensions;

namespace MasonPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogEntryController : ControllerBase
    {
        private readonly ILogger<LogEntryController> _logger;
        private readonly ILogEntryService _logEntryService;

        public LogEntryController(ILogger<LogEntryController> logger, ILogEntryService logEntryService)
        {
            _logger = logger;
            _logEntryService = logEntryService;
        }
        /// <summary>
        /// 查下列表
        /// </summary>
        /// <param name="logEntryReq"></param>
        /// <returns></returns>

        [HttpGet("list")]
        public async Task<Pack<List<LogEntry>>> List([FromQuery]LogEntryReq logEntryReq)
        {
            return await _logEntryService.LogEntrisAsync(logEntryReq);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Detail")]
        public async Task<Pack<LogEntry>> Detail(string id)
        {
            return await _logEntryService.LogEntryAsync(id);
        }
    }
}