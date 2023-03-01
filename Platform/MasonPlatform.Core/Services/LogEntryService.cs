using MasonPlatform.Models.Dtos;
using MasonPlatform.Models.Entities;
using SharpMason.Extensions;

namespace MasonPlatform.Core.Services
{
    public class LogEntryService : ILogEntryService
    {
        public Task<Pack<List<LogEntry>>> LogEntrisAsync(LogEntryReq logEntryReq)
        {
            throw new NotImplementedException();
        }

        public Task<Pack<LogEntry>> LogEntryAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
