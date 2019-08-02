using HTTP.Enums;
using IRunes.Data;
using WebServer;
using WebServer.Results;
using WebServer.Routing;

namespace IRunes.App
{

    public class Launcher
    {
        public static void Main()
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void Configure(ServerRoutingTable serverRoutingTable)
        {
            serverRoutingTable.Add(HttpRequestMethod.GET, "/", request => new RedirectResult("/Home/Index"));
        }
    }
}
