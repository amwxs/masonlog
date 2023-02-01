using SharpMason.Logging.DiskFile;
using System.Collections.Concurrent;

namespace SharpMason.Logging
{
    public class Processor
    {
        private readonly Thread _outputThread;
        private readonly BlockingCollection<LogEntry> _messageQueue;
        private readonly ILoggerWriter _loggerWriter;
        private readonly IFileWriter _fileWriter;
        public Processor(int maxQuene, ILoggerWriter loggerWriter, IFileWriter fileWriter)
        {
            _loggerWriter = loggerWriter;
            _fileWriter = fileWriter;
            _messageQueue = new BlockingCollection<LogEntry>(maxQuene);

            _outputThread = new Thread(Consumer)
            {
                IsBackground = true,
                Name = "Console logger queue processing thread",
            };
            _outputThread.Start();
        }

        public void Enqueue(LogEntry log)
        {
            if (!_messageQueue.TryAdd(log))
            {
                _fileWriter.Writer(new FileEntry("添加日志失败!"));
            };
        }
        private void Consumer()
        {
            try
            {
                foreach (var log in _messageQueue.GetConsumingEnumerable())
                {

                    _loggerWriter.Write(log);
                }
            }
            catch(Exception e)
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch{
                }
                _fileWriter.Writer(new FileEntry("消费日志队列失败!" + e.ToString()));
            }
        }
        public void Dispose()
        {
            try
            {
                //阻塞主线程 刷新日志
                _outputThread.Join(1000);
            }
            catch (ThreadStateException) { }

            _messageQueue.Dispose();
        }
    }
}
