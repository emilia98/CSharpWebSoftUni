using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FDMC.Models;
using FDMC.Data;
using Microsoft.EntityFrameworkCore;

namespace FDMC.Controllers
{
    public class HomeController : Controller
    {
        public CatsDbContext Context { get; set; }

        public HomeController(CatsDbContext context)
        {
            this.Context = context;
        }

        public async Task<IActionResult> Index()
        {
            var length = 0;
            try
            {
                var cats = await this.Context.Cats.ToListAsync();
                length = cats.Count();
                this.ViewBag.Cats = cats;
                return View();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return this.Redirect("/");
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
