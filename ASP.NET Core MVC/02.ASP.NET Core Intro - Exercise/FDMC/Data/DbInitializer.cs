using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CatsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
