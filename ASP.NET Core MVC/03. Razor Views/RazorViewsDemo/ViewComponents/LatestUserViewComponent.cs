using Microsoft.AspNetCore.Mvc;
using RazorViewsDemo.Models.Home;
using RazorViewsDemo.Services;

namespace RazorViewsDemo.ViewComponents
{
    public class LatestUserViewComponent : ViewComponent
    {
        private readonly IUsersService usersService;

        public LatestUserViewComponent(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        /*
        public async Task<IViewComponentResult> Invoke()
        {
            return this.View(this.usersService.LatestUsername());
        }
        */
        public IViewComponentResult Invoke(string text)
        {
            if(text.Length == 0)
            {
                text = "Hello";
            }

            return this.View(
                new LatestUserViewComponentViewModel {
                    Text = text,
                    Username = this.usersService.LatestUsername() }
            );
        }
    }
}
