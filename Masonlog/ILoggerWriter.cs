namespace SharpMason.Logging
{
    public interface ILoggerWriter
    {
        void Write(LogEntry logEntry);
    }
}
