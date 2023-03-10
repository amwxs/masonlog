using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace SharpMason.Logging.Test;

public class LogExtensionsTest
{
    [Fact]
    public void AddLoggerTest()
    {
        var builderMoq = new Mock<ILoggingBuilder>();
        builderMoq.Setup(x => x.Services).Returns(new ServiceCollection());
        var builder = builderMoq.Object;
        builder.AddSharpMasonLogger();

    }
    
    [Fact]
    public void AddLoggerTest2()
    {
        var builderMoq = new Mock<ILoggingBuilder>();
        builderMoq.Setup(x => x.Services).Returns(new ServiceCollection());
        var builder = builderMoq.Object;
        builder.AddSharpMasonLogger(cfg =>
        {
            cfg.AppId = "test";
            cfg.IsConsole = true;
        } );

    }

}