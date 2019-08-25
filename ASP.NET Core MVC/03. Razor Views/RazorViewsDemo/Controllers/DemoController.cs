using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RazorViewsDemo.Models.Home;
using RazorViewsDemo.Services;

namespace RazorViewsDemo.Controllers
{
    public class DemoController : Controller
    {
        private readonly IUsersService usersService;

        public DemoController(IUsersService usersService)
        {
            this.usersService = usersService;

        }

        [HttpGet("/demo/htmlraw")]
        public IActionResult HtmlRaw()
        {
            return View();
        }

        [HttpGet("/demo/di")]
        public IActionResult UsingDI()
        {
            /* var usernames = this.usersService.GetUsernames();*/
            return this.View();
        }


        [HttpGet("/demo/novm")]
        public IActionResult NoViewModel()
        {
            return this.View(this.usersService.GetUsernames().ToList());
        }

        public IActionResult Usernames()
        {
            var usernames = this.usersService.GetUsernames().ToList();
            var viewModel = new DemoViewModel { Usernames = usernames };
            return this.View(viewModel);
        }

        [HttpGet("/demo/nolayout")]
        public IActionResult NoLayout()
        {
            return this.View();
        }

        public IActionResult Section()
        {
            return this.View();
        }

        public IActionResult HtmlHelpers()
        {
            return this.View();
        }

        public IActionResult TagHelpers()
        {
            return this.View();
        }

        public IActionResult Partial1()
        {
            var usernames = this.usersService.GetUsernames().ToList();
            var viewModel = new DemoViewModel { Usernames = usernames };
            return this.View(viewModel);
        }

        public IActionResult Partial2()
        {
            var usernames = this.usersService.GetUsernames().ToList();
            var viewModel = new DemoViewModel { Usernames = usernames };
            return this.View(viewModel);
        }

        public IActionResult ViewComponent()
        {
            return this.View();
        }
    }
}