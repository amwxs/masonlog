
namespace SharpMason.Extensions.Context
{
    public class ContextAccessor
    {
        private static readonly AsyncLocal<Context> _current = new();

        public static Context Current => _current.Value;
        public static void Clear() => _current.Value = null;

        public static IDisposable Build(Context context)
        {
            _current.Value = context;
            return new ContextDisposable();
        }

    }
}
