using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace SharpMason.Logging.Kafka
{
    public class KafkaClient : IKafkaClient
    {
        private IProducer<Null, string> _producer;
        private KafkaOption _kafkaOption;
        private readonly IDisposable? _onChangeToken;

        public KafkaClient(IOptionsMonitor<KafkaOption> options)
        {
            _kafkaOption = options.CurrentValue;
            _onChangeToken = options.OnChange(ReBuild);
            _producer = Build();
        }
        /// <summary>
        /// 构建Producer
        /// </summary>
        public IProducer<Null, string> Producer()
        {
            return _producer;
        }

        private IProducer<Null, string> Build() 
        {
            return new ProducerBuilder<Null, string>(new ProducerConfig
            {
                BootstrapServers = _kafkaOption.BootstrapServers,
                SaslMechanism = (SaslMechanism)_kafkaOption.SaslMechanism,
                SecurityProtocol = (SecurityProtocol)_kafkaOption.SecurityProtocol,
                SaslUsername = _kafkaOption.SaslUsername,
                SaslPassword = _kafkaOption.SaslPassword,
                

            }).Build();
        }

        //根据配置构建新的Producer
        private void ReBuild(KafkaOption option)
        {
            _kafkaOption = option;

            var oldProducer = _producer;
            _producer = Producer();

            //数据推送到Broker
            oldProducer.Flush();
            //释放资源
            oldProducer.Dispose();
        }

        public void Dispose()
        {
            _onChangeToken?.Dispose();

            //释放kafka资源
            _producer.Flush();
            _producer.Dispose();

        }
    }
}
