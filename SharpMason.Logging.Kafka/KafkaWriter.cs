using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SharpMason.Extensions.Utils;

namespace SharpMason.Logging.Kafka
{
    public class KafkaWriter : ILoggerWriter, IDisposable
    {
        private readonly IKafkaClient _kafkaClient;

        private readonly IOptions<KafkaOption> _options;


        public KafkaWriter(IKafkaClient kafkaClient, IOptions<KafkaOption> options)
        {
            _kafkaClient = kafkaClient;
            _options = options;
        }
        //异步写入回调
        private void AsyncHandler(DeliveryReport<Null, string> deliveryReport)
        {
            if (deliveryReport.Error.IsError)
            {

            }
        }

        public void Write(LogEntry logEntry)
        {
            var producer = _kafkaClient.Producer();
            //异步写入
            producer.Produce(
               _options.Value.Topic,
               new Message<Null, string>() { Value = logEntry.ToJson() }, AsyncHandler);
        }

        public void Dispose()
        {
            _kafkaClient?.Dispose();
        }
    }
}
