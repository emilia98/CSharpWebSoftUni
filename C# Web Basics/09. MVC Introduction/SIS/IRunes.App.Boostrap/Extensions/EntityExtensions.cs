using IRunes.Models;
using System.Linq;
using System.Net;
using System.Text;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlAll(this Album album)
        {
            return $"<a class=\"h5\" href=\"/Albums/Details?id={album.Id}\">{WebUtility.UrlDecode(album.Name)}</a>";
        }

        public static string ToHtmlDetails(this Album album)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<div class=\"album-details row\">")
              .Append($"  <div class=\"album-data col-md-6 text-center\">")
              .Append($"    <img class=\"img-thumbnail\" src=\"{WebUtility.UrlDecode(album.Cover)}\"/>")
              .Append($"    <h3 class=\"text-center mt-3\">Album Name: {WebUtility.UrlDecode(album.Name)}</h3>")
              .Append($"    <h3 class=\"text-center\">Album Price: ${album.Price}</h3>")
              .Append($"    <div class=\"d-flex justify-content-between\">")
              .Append($"      <a class=\"btn btn-success text-white\" href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>")
              .Append($"      <a class=\"btn btn-success text-white\" href=\"/Albums/All\">Back To All</a>")
              .Append($"    </div>")
              .Append($"  </div>")
              .Append($"  <div class=\"album-tracks col-md-6\">")
              .Append($"    <h2>Tracks</h2>")
              .Append($"    <ul class=\"tracks-list\">");

            if(album.Tracks.Count == 0)
            {
                sb.Append("<li class=\"text-center\">No tracks to show</li>");
            }
            else
            {
                sb.Append(string.Join("", album.Tracks.Select((track, index) => track.ToHtmlAll(index + 1, album.Id))));
            }
            sb.Append($"    </ul>")
              .Append($"  </div>")
              .Append($"</div>");

            return sb.ToString();
        }

        public static string ToHtmlAll(this Track track, int index, string albumId)
        {
            return $"<li><strong>{index}. <a class=\"font-italic\" href=\"/Tracks/Details?albumId={albumId}&trackId={track.Id}\">{WebUtility.UrlDecode(track.Name)}</a></strong></li>";
        }

        public static string ToHtmlDetails(this Track track)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"track-details\">")
              .Append($"  <h4 class=\"text-center\">Track Name: {WebUtility.UrlDecode(track.Name)}</h4>")
              .Append($"  <h4 class=\"text-center\">Track Price: ${track.Price}</h4>")
              .Append($"  <hr class=\"bg-success\" style=\"height: 2px\" />")
              .Append($"  <div class=\"embed-responsive embed-responsive-16by9\">")
              .Append($"    <iframe class=\"embed-responsive-item\" src=\"{WebUtility.UrlDecode(track.Link)}\"></iframe>")
              .Append($"  </div>")
              .Append($"  <hr class=\"bg-success\" style=\"height: 2px\" />")
              .Append("</div>");

            return sb.ToString();
        }
    }
}
