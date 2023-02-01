using Confluent.Kafka;

namespace SharpMason.Logging.Kafka
{
    public interface IKafkaClient : IDisposable
    {
        /// <summary>
        /// 获取生产者
        /// </summary>
        /// <returns></returns>
        IProducer<Null, string> Producer();
    }
}