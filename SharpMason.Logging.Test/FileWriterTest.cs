using Microsoft.Extensions.Options;
using SharpMason.Logging.DiskFile;

namespace SharpMason.Logging.Test;

public class FileWriterTest
{
    [Fact]
    public void FileWriter_Test()
    {
        var optionMock =new Moq.Mock<IOptionsMonitor<LoggerOption>>();
        optionMock.Setup(x => x.CurrentValue).Returns(new LoggerOption()
        {
            AppId = "file-appid-log",
            LocalFilePath = @"e:\var"
        });
        var path = Path.Combine(optionMock.Object.CurrentValue.LocalFilePath,
            optionMock.Object.CurrentValue.AppId!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
        
        var fileWriter = new FileWriter(optionMock.Object);
        var fileEntry = new FileEntry("hello");
        fileWriter.Writer(fileEntry);
        Thread.Sleep(2000);
        fileWriter.Dispose();

        var filePath = Path.Combine(optionMock.Object.CurrentValue.LocalFilePath,
            optionMock.Object.CurrentValue.AppId!, fileEntry.LogName);

        var contents = File.ReadLines(filePath);
        Assert.NotNull(contents);
        Assert.True(contents.Last() == "hello");
    }
}