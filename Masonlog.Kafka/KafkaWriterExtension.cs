using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace SharpMason.Logging.Kafka
{
    public static class KafkaWriterExtension
    {
        public static ILoggingBuilder AddKafkaWriter(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();
            builder.Services.Configure<KafkaOption>(configuration.GetSection("KafkaWriter"));

            builder.Services.AddSingleton<IKafkaClient, KafkaClient>();


            builder.Services.RemoveAll<ILoggerWriter>();
            builder.Services.AddSingleton<ILoggerWriter, KafkaWriter>();
            return builder;
        }

        public static ILoggingBuilder AddKafkaWriter(this ILoggingBuilder builder, Action<KafkaOption> configure)
        {
            builder.Services.Configure(configure);

            builder.Services.RemoveAll<ILoggerWriter>();
            builder.Services.AddSingleton<ILoggerWriter, KafkaWriter>();
            builder.Services.AddSingleton<IKafkaClient, KafkaClient>();
            return builder;
        }
    }
}
