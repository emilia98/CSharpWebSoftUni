﻿using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("/cat/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            bool isParsed = int.TryParse(id, out int Id);

            try
            {
                if(!isParsed)
                {
                    throw new Exception("Invalid Id!");
                }

                var cat = await this.Context.Cats.FirstOrDefaultAsync(c => c.Id == Id);

                if(cat == null)
                {
                    return NotFound();
                }

                this.Context.Cats.Remove(cat);
                await this.Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return this.Redirect("/");
        }

        [HttpGet("/cat/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            bool isParsed = int.TryParse(id, out int Id);

            try
            {
                if(!isParsed)
                {
                    throw new Exception("Invalid Id!");
                }

                var cat = await this.Context.Cats.FirstOrDefaultAsync(c => c.Id == Id);

                if(cat == null)
                {
                    return NotFound();
                }

                this.ViewBag.Cat = cat;

                return this.View();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return this.Redirect("/");
        }
    }
}
