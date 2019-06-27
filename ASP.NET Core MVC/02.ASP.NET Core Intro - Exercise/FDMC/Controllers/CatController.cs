using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Controllers
{
    public class CatController : Controller
    {
        public CatsDbContext Context { get; set; }

        public CatController(CatsDbContext context)
        {
            this.Context = context;
        }

        [HttpGet("/cats/add")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost("/cats/add")]
        public async Task<IActionResult> AddPost(string name, string breed, int age, string imageUrl)
        {
            try
            {
                 this.Context.Cats.Add(new Cat()
                {
                    Name = name,
                    Breed = breed,
                    Age = age,
                    ImageUrl = imageUrl
                });

                await this.Context.SaveChangesAsync();
                return this.Redirect("/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return this.Redirect("/");
            }

           


            
            //return this.Json(cat);
        }
    }
}
