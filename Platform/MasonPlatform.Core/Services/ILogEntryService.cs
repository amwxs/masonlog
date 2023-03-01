using MasonPlatform.Models.Dtos;
using MasonPlatform.Models.Entities;
using SharpMason.Extensions;

namespace MasonPlatform.Core.Services
{
    public interface ILogEntryService
    {
        Task<Pack<List<LogEntry>>> LogEntrisAsync(LogEntryReq logEntryReq);

        Task<Pack<LogEntry>> LogEntryAsync(string id);
    }
}