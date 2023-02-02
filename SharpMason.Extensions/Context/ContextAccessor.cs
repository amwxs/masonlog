
namespace SharpMason.Extensions.Context
{
    public abstract class ContextAccessor
    {
        private static readonly AsyncLocal<Context?> Local = new();

        public static Context? Current => Local.Value;
        public static void Clear() => Local.Value = null;

        public static IDisposable Build(Context context)
        {
            Local.Value = context;
            return new ContextDisposable();
        }

    }
}
