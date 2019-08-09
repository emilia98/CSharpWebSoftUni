using MvcFramework.Routing;

namespace MvcFramework
{
    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices(); // DI
    }
}
