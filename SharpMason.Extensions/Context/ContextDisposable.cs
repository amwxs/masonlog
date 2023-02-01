namespace SharpMason.Extensions.Context
{
    public class ContextDisposable : IDisposable
    {
        public void Dispose()
        {
            ContextAccessor.Clear();
        }
    }
}
