namespace SharpMason.Discovery
{
    public interface IServiceDiscovery
    {

         List<string> Address(string serviceName);
    }
}