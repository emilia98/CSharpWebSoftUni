using System.Collections.Generic;

namespace RazorViewsDemo.Models.Home
{
    public class DemoViewModel
    {
        public IEnumerable<string> Usernames { get; set; }

        public string Name { get; } = nameof(DemoViewModel);
    }
}
