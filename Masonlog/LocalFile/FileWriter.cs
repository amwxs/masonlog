using Microsoft.Extensions.Options;
using SharpMason.Logging;
using System.Collections.Concurrent;

namespace Masonlog.LocalFile
{
    public class FileWriter : IFileWriter
    {
        private StreamWriter? _writer;
        private readonly CancellationTokenSource _cts = new();
        private readonly BlockingCollection<FileEntry> _messageQueue;
        private readonly string _folder;
        public FileWriter(IOptionsMonitor<LoggerOption> options)
        {
            _messageQueue = new BlockingCollection<FileEntry>(1024);
            _folder = Path.Combine(options.CurrentValue.LocalFilePath, options.CurrentValue.AppId!);
            if (!string.IsNullOrWhiteSpace(_folder) && !Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
            var task = new Task(_ => Consumer(), _cts, TaskCreationOptions.LongRunning);
            task.Start();
            Writer(new FileEntry("Init FileWriter Success!"));
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
                    if (_messageQueue.Count > 0)
                    {
                        var fileName = string.Empty;
                        var count = 0;
                        while (_messageQueue.TryTake(out var log, 10))
                        {
                            if (_writer == null)
                            {
                                _writer = new StreamWriter(Path.Combine(_folder, log.LogName), true);
                            }
                            else
                            {
                                if (fileName != log.LogName)
                                {
                                    _writer.Close();//这里会自动写入磁盘
                                    _writer = new StreamWriter(Path.Combine(_folder, log.LogName), true);
                                    fileName = log.LogName;
                                }
                            }
                            //写信息
                            _writer.WriteLine(log.Msg);
                            if (count++ > 100)
                            {
                                //大于100条写入磁盘
                                _writer.Flush();
                            }
                        }
                        //没有数据关闭流写入磁盘
                        _writer?.Close();
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
            _writer?.Close();
            _cts.Cancel();
            _messageQueue.Dispose();
        }
    }
}
