using System.Collections.Generic;

namespace RazorViewsDemo.Services
{
    public interface IUsersService
    {
        int GetCount();

        IEnumerable<string> GetUsernames();

        string LatestUsername();
    }
}
