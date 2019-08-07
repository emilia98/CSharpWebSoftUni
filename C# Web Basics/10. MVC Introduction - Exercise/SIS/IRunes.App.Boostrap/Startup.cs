using IRunes.Data;
using MvcFramework;
using MvcFramework.Routing;

namespace IRunes.App
{
    public class Startup : IMvcApplication
    {
        public Startup()
        {
        }

        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }
        }


        public void ConfigureServices()
        {

        }
    }
}