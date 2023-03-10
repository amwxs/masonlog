using Confluent.Kafka;

namespace SharpMason.Logging.Kafka
{

    public class KafkaOption
    {
        public string? BootstrapServers { get; set; }
        public string Topic { get; set; } = "mason-log";
        public SaslMechanism SaslMechanism { get; set; }
        public SecurityProtocol SecurityProtocol { get; set; }
        public string? SaslUsername { get; set; }
        public string? SaslPassword { get; set; }
    }
}
