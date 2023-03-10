namespace SharpMason.Logging.Test;

public class NullLoggerWriterTest
{
    [Fact]
    public void NullLoggerTest()
    {
        var nullLogger = new NullLoggerWriter();
        nullLogger.Write(new LogEntry());

        var logger = nullLogger as ILoggerWriter;
        Assert.NotNull(logger);
    }
}