using RazorViewsDemo.Data;
using System.Collections.Generic;
using System.Linq;

namespace RazorViewsDemo.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int GetCount()
        {
            return this.dbContext.Users.Count();
        }

        public IEnumerable<string> GetUsernames()
        {
            return this.dbContext.Users.Select(x => x.UserName).ToList();
        }
    }
}
