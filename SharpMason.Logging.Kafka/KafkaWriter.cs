using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SharpMason.Extensions.Utils;
using SharpMason.Logging.DiskFile;

namespace SharpMason.Logging.Kafka
{
    public class KafkaWriter : ILoggerWriter, IDisposable
    {
        private readonly IKafkaClient _kafkaClient;

        private readonly IOptions<KafkaOption> _options;
        private readonly IFileWriter _fileWriter;


        public KafkaWriter(IKafkaClient kafkaClient, IOptions<KafkaOption> options,IFileWriter fileWriter)
        {
            _kafkaClient = kafkaClient;
            _options = options;
            _fileWriter = fileWriter;
        }
        //异步写入回调
        private void AsyncHandler(DeliveryReport<Null, string> deliveryReport)
        {
            if (deliveryReport.Error.IsError)
            {
                _fileWriter.Writer(new FileEntry(deliveryReport.Error.ToJson(),"kafka_error"));
            }
        }

        public void Write(LogEntry logEntry)
        {
            //异步写入
            _kafkaClient.Producer().Produce(
               _options.Value.Topic,
               new Message<Null, string>() { Value = logEntry.ToJson() }, AsyncHandler);
        }

        public void Dispose()
        {
            _kafkaClient?.Dispose();
        }
    }
}
