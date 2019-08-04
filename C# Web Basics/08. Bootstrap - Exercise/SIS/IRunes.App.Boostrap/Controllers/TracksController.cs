using HTTP.Requests;
using HTTP.Responses;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace IRunes.App.Controllers
{
    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = ((IList<string>)httpRequest.QueryData["albumId"])[0];

            this.ViewData["AlbumId"] = albumId;
            return this.View();
        }

        public IHttpResponse CreatePost(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = ((IList<string>)httpRequest.QueryData["albumId"])[0];

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.Include(a => a.Tracks).SingleOrDefault(album => album.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                string name = ((IList<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string link = ((IList<string>)httpRequest.FormData["link"]).FirstOrDefault();
                string price = ((IList<string>)httpRequest.FormData["price"]).FirstOrDefault();

                string decodedLink = WebUtility.UrlDecode(link);

                if (decodedLink.StartsWith("https://www.youtube.com/watch"))
                {
                    string pattern = @"v=(.+)&*";
                    Regex regex = new Regex(pattern);

                    var match = regex.Match(decodedLink);
                    var videoId = match.Value.Replace("v=", "");
                    link = $"https://www.youtube.com/embed/{videoId}";
                }

                Track track = new Track()
                {
                    Name = name,
                    Link = link,
                    Price = decimal.Parse(price)
                };

                albumFromDb.Tracks.Add(track);
                albumFromDb.Price = 0.87m * albumFromDb.Tracks.Select(t => t.Price).Sum();

                context.Tracks.Add(track);
                context.Update(albumFromDb);
                context.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = ((IList<string>)httpRequest.QueryData["albumId"])[0];
            string trackId = ((IList<string>)httpRequest.QueryData["trackId"])[0];

            using (var context = new RunesDbContext())
            {
                Track trackFromDb = context.Tracks.SingleOrDefault(track => track.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect($"/Albums/Details?id={albumId}");
                }

                this.ViewData["AlbumId"] = albumId;
                this.ViewData["Track"] = trackFromDb.ToHtmlDetails();
            }

            return this.View();
        }
    }
}
