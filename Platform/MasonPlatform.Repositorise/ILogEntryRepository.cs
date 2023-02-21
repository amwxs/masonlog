using MasonPlatform.Models.Dtos;
using MasonPlatform.Models.Entities;

namespace MasonPlatform.Repositorise
{
    public interface ILogEntryRepository
    {
        List<LogEntry> GetLogEntries();
        int Count();
    }
}
