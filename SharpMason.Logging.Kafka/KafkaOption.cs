namespace SharpMason.Logging.Kafka
{

    public class KafkaOption
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; } = "amld-log";
        public int SaslMechanism { get; set; } = 1;
        public int SecurityProtocol { get; set; } = 2;
        public string SaslUsername { get; set; }
        public string SaslPassword { get; set; }
    }
}
