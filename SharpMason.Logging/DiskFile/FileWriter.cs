using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace SharpMason.Logging.DiskFile
{
    public class FileWriter : IFileWriter
    {
        private readonly BlockingCollection<FileEntry> _messageQueue;
        private string _folder;
        private readonly IDisposable? _onChangeToken;
        public FileWriter(IOptionsMonitor<LoggerOption> options)
        {
            CreateOrUpdateDirectory(options.CurrentValue);

            _messageQueue = new BlockingCollection<FileEntry>(1024);
            var outputThread = new Thread(Consumer)
            {
                IsBackground = true,
                Name = "file queue processing thread"
            };
            outputThread.Start();


            _onChangeToken = options.OnChange(CreateOrUpdateDirectory);

            Writer(new FileEntry("init FileWriter success"));
        }

        private void CreateOrUpdateDirectory(LoggerOption loggerOption)
        {
            _folder = Path.Combine(loggerOption.LocalFilePath, loggerOption.AppId);

            if (!string.IsNullOrWhiteSpace(_folder) && !Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
        }

        public void Writer(FileEntry fileEntry)
        {
             _messageQueue.TryAdd(fileEntry);
        }
        private void Consumer()
        {

            try
            {
                while (true)
                {
                    if (_messageQueue.Count>0)
                    {
                        StreamWriter? writer = null;
                        var fileName = string.Empty;
                        var count = 0;
                        while (_messageQueue.TryTake(out var log, 10))
                        {
                            if (writer == null)
                            {
                                writer = new StreamWriter(Path.Combine(_folder, log.FileName), true);
                            }
                            else
                            {
                                if (fileName != log.FileName)
                                {
                                    writer.Close();
                                    writer = new StreamWriter(Path.Combine(_folder, log.FileName), true);
                                    fileName = log.FileName;
                                }
                            }

                            writer.WriteLine(log.Msg);
                            if (count++ > 100)
                            {
                                break;
                            }
                        }
                        writer?.Close();
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);//ignore
            }
        }


        public void Dispose()
        {
            _onChangeToken?.Dispose();
            _messageQueue.Dispose();
        }
    }
}
