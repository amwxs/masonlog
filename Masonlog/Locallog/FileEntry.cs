namespace Masonlog.LocalFile
{
    public class FileEntry
    {
        public string LogName { get; }
        public string Msg { get; }
        public FileEntry(string msg, string logName = "log")
        {
            LogName = $"{logName}{DateTime.Now:yyyyMMdd}.log";
            Msg = msg;
        }
    }
}
