namespace SharpMason.Extensions.Context
{
    public class Context
    {
        private Dictionary<string, object> Items { get; set; } = new();

        public T TryGetValue<T>(string key)
        {
            if (Items.TryGetValue(key, out var value))
            {
                if (value is T t)
                {
                    return t;
                }
            }
            return default;
        }

        public bool TryAdd(string key, object value)
        {
            return Items.TryAdd(key, value);
        }
    }
}
