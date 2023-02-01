using SharpMason.Logging.DiskFile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace SharpMason.Logging
{
    public static class LogExtensions
    {
        public static ILoggingBuilder AddAmldLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddSingleton<IFileWriter, FileWriter>();
            builder.Services.TryAddSingleton<ILoggerWriter, NullLoggerWriter>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<LoggerOption, LoggerProvider>(builder.Services);
            return builder;
        }

        public static ILoggingBuilder AddAmldLogger(this ILoggingBuilder builder,Action<LoggerOption> configure)
        {
            builder.AddAmldLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
