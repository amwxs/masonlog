namespace Masonlog.Enhancer
{
    public abstract class LogContextAccessor
    {
        private static readonly AsyncLocal<LogContext?> Local = new();

        public static LogContext? Current => Local.Value;
        public static void Clear() => Local.Value = null;

        public static IDisposable Build(LogContext context)
        {
            Local.Value = context;
            return new LogContextDisposable();
        }

    }
}
