namespace Masonlog.Enhancer
{
    public class LogContextDisposable : IDisposable
    {
        public void Dispose()
        {
            LogContextAccessor.Clear();
        }
    }
}
