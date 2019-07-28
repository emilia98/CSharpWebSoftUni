using Demo.Controllers;
using HTTP.Enums;
using WebServer;
using WebServer.Routing;

namespace Demo
{
    class Launcher
    {
        public static void Main()
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.GET, "/", httpRequest => new HomeController().Home(httpRequest));

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
