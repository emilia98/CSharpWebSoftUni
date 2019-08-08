using IRunes.App.Extensions;
using IRunes.App.ViewModels;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Mapping;
using MvcFramework.Results;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult All()
        {
            ICollection<Album> albums = this.albumService.GetAllAlbums();

            if (albums.Count == 0)
            {
                this.ViewData["Albums"] = "There are curently no albums.";
            }
            else
            {
                this.ViewData["Albums"] = string.Join("<br />", albums.Select(album => album.ToHtmlAll()).ToList());
            }

            return this.View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreatePost()
        {
            string name = ((IList<string>)this.Request.FormData["name"]).FirstOrDefault();
            string cover = ((IList<string>)this.Request.FormData["cover"]).FirstOrDefault();

            Album album = new Album()
            {
                Name = name,
                Cover = cover,
                Price = 0m
            };

            this.albumService.CreateAlbum(album);

            return this.Redirect("/Albums/All");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = ((IList<string>)this.Request.QueryData["id"])[0];
            Album albumFromDb = this.albumService.GetAlbumById(albumId);

            AlbumViewModel albumViewModel = ModelMapper.ProjectTo<AlbumViewModel>(albumFromDb);

            if (albumFromDb == null)
            {
                return this.Redirect("/Albums/All");
            }

            this.ViewData["Album"] = albumFromDb.ToHtmlDetails();

            return this.View();
        }
    }
}
