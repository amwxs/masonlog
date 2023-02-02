using SharpMason.Logging.DiskFile;
using Moq;

namespace SharpMason.Logging.Test
{
    public class ProcessorTest
    {
        [Fact]
        public void EnqueueTest()
        {
            var loggerWriterMock = new Mock<ILoggerWriter>();
            var fileWriterMock = new Mock<IFileWriter>();
            var proc = new Processor(5000, loggerWriterMock.Object, fileWriterMock.Object);
            proc.Enqueue(new LogEntry
            {
                AppId = "SharpMason"
            });

        }
    }
}
