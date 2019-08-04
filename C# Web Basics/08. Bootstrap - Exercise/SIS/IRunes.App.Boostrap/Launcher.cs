using HTTP.Enums;
using IRunes.App.Controllers;
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
            #region Home Routes

            serverRoutingTable.Add(HttpRequestMethod.GET, "/", request => new RedirectResult("/Home/Index"));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Home/Index", request => new HomeController().Index(request));

            #endregion

            #region Users Routes

            serverRoutingTable.Add(HttpRequestMethod.GET, "/Users/Login", request => new UsersController().Login(request));
            serverRoutingTable.Add(HttpRequestMethod.POST, "/Users/Login", request => new UsersController().LoginPost(request));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Users/Register", request => new UsersController().Register(request));
            serverRoutingTable.Add(HttpRequestMethod.POST, "/Users/Register", request => new UsersController().RegisterPost(request));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Users/Logout", request => new UsersController().Logout(request));

            #endregion

            #region Albums Routes

            serverRoutingTable.Add(HttpRequestMethod.GET, "/Albums/All", request => new AlbumsController().All(request));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Albums/Create", request => new AlbumsController().Create(request));
            serverRoutingTable.Add(HttpRequestMethod.POST, "/Albums/Create", request => new AlbumsController().CreatePost(request));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Albums/Details", request => new AlbumsController().Details(request));

            #endregion

            #region Tracks Region

            serverRoutingTable.Add(HttpRequestMethod.GET, "/Tracks/Create", request => new TracksController().Create(request));
            serverRoutingTable.Add(HttpRequestMethod.POST, "/Tracks/Create", request => new TracksController().CreatePost(request));
            serverRoutingTable.Add(HttpRequestMethod.GET, "/Tracks/Details", request => new TracksController().Details(request));

            #endregion
        }
    }
}
