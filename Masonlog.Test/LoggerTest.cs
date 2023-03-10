using Moq;
using Masonlog.LocalFile;
using SharpMason.Logging.Utils;

namespace SharpMason.Logging.Test
{
    public class LoggerTest
    {
        [Fact]
        public void GetKeyPrefixesTest()
        {

            var name = "Business.Services.TextEncoderService";

            var loggerWriterMock = new Mock<ILoggerWriter>();
            var fileWriterMock = new Mock<IFileWriter>();
            var _hostIP = NetWorkUtil.GetHostIp();
            var logger = new Logger(new Processor(100, loggerWriterMock.Object, fileWriterMock.Object),name, new LoggerOption(), _hostIP);
           
            var listKeyPrefix = new List<string>
            {
                "Business.Services.TextEncoderService",
                "Business.Services",
                "Business",
                "Default",
            };

            var collectPerfixs = new List<string>();
            foreach (var keyPrefix in logger.GetKeyPrefix(name))
            {
                collectPerfixs.Add(keyPrefix);
            }


            Assert.Equal(listKeyPrefix.Count, collectPerfixs.Count);
            for (int i = 0; i < listKeyPrefix.Count; i++)
            {
                Assert.Equal(listKeyPrefix[i], collectPerfixs[i]);
            }
        }
    }
}
