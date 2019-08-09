using IRunes.App.Extensions;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService trackService;

        private readonly IAlbumService albumService;

        public TracksController()
        {
            this.trackService = new TrackService();
            this.albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult Create()
        {
            string albumId = ((IList<string>)this.Request.QueryData["albumId"])[0];

            this.ViewData["AlbumId"] = albumId;
            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreatePost()
        {
            string albumId = ((IList<string>)this.Request.QueryData["albumId"])[0];
            string name = ((IList<string>)this.Request.FormData["name"]).FirstOrDefault();
            string link = ((IList<string>)this.Request.FormData["link"]).FirstOrDefault();
            string price = ((IList<string>)this.Request.FormData["price"]).FirstOrDefault();

            string decodedLink = WebUtility.UrlDecode(link);

            if (decodedLink.StartsWith("https://www.youtube.com/watch"))
            {
                string pattern = @"v=(.+)&*";
                Regex regex = new Regex(pattern);

                var match = regex.Match(decodedLink);
                var videoId = match.Value.Replace("v=", "");
                link = $"https://www.youtube.com/embed/{videoId}";
            }

            Track trackForDb = new Track()
            {
                Name = name,
                Link = link,
                Price = decimal.Parse(price)
            };

            if(!this.albumService.AddTrackToAlbum(albumId, trackForDb))
            {
                return this.Redirect("/Albums/All");
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = ((IList<string>)this.Request.QueryData["albumId"])[0];
            string trackId = ((IList<string>)this.Request.QueryData["trackId"])[0];

            Track trackFromDb = this.trackService.GetTrackById(trackId);

            if (trackFromDb == null)
            {
                return this.Redirect($"/Albums/Details?id={albumId}");
            }

            this.ViewData["AlbumId"] = albumId;
            this.ViewData["Track"] = trackFromDb.ToHtmlDetails();

            return this.View();
        }
    }
}
