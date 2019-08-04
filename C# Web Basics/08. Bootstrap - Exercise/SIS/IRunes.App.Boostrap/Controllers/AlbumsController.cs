using HTTP.Requests;
using HTTP.Responses;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            if(!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using(var context = new RunesDbContext())
            {
                ICollection<Album> albums = context.Albums.ToList();

                if(albums.Count == 0)
                {
                    this.ViewData["Albums"] = "There are curently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] = string.Join("<br />", albums.Select(album => album.ToHtmlAll()).ToList());
                }
                
                return this.View();
            }
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public IHttpResponse CreatePost(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using(var context = new RunesDbContext())
            {
                string name = ((IList<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string cover = ((IList<string>)httpRequest.FormData["cover"]).FirstOrDefault();

                Album album = new Album()
                {
                    Name = name,
                    Cover = cover,
                    Price = 0m
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return this.Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = ((IList<string>)httpRequest.QueryData["id"])[0];

            using(var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.Include(a => a.Tracks).SingleOrDefault(album => album.Id == albumId);

                if(albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                this.ViewData["Album"] = albumFromDb.ToHtmlDetails();
            }

            return this.View();
        }
    }
}
