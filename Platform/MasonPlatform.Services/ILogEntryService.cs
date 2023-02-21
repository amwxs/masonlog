using MasonPlatform.Models.Dtos;
using MasonPlatform.Models.Entities;
using SharpMason.Extensions;

namespace MasonPlatform.Services
{
    public interface ILogEntryService
    {
        Task<Pack<List<LogEntry>>> LogEntrisAsync(LogEntryReq logEntryReq);

        Task<Pack<LogEntry>> LogEntryAsync(string id);
    }
}