using MasonPlatform.Models.Entities;

namespace MasonPlatform.Core.Repositorise
{
    public interface ILogEntryRepository
    {
        List<LogEntry> GetLogEntries();
        int Count();
    }
}
