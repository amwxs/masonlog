namespace Masonlog.Enhancer
{
    public class LogContext
    {
        private Dictionary<string, object> Items { get; } = new();

        public T? TryGetValue<T>(string key)
        {
            if (!Items.TryGetValue(key, out var value)) return default;
            if (value is T t)
            {
                return t;
            }

            return default;
        }

        public bool TryAdd(string key, object value)
        {
            return Items.TryAdd(key, value);
        }
    }
}
