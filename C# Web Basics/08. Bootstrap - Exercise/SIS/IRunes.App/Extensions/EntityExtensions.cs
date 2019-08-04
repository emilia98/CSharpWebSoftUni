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
            return $"<a href=\"/Albums/Details?id={album.Id}\">{WebUtility.UrlDecode(album.Name)}</a>";
        }

        public static string ToHtmlDetails(this Album album)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<div class=\"album-details\">")
              .Append($"  <div class=\"album-data\">")
              .Append($"    <img src=\"{WebUtility.UrlDecode(album.Cover)}\"/>")
              .Append($"    <h3>Album Name: {WebUtility.UrlDecode(album.Name)}</h3>")
              .Append($"    <h3>Album Price: ${album.Price}</h3>")
              .Append($"    <br/>")
              .Append($"  </div>")
              .Append($"  <div class=\"album-tracks\">")
              .Append($"    <h3>Tracks</h3>")
              .Append($"    <hr style=\"height: 2px\" />")
              .Append($"    <a href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>")
              .Append($"    <hr style=\"height: 2px\" />")
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
              .Append($"    <hr style=\"height: 2px\" />")
              .Append($"  </div>")
              .Append($"</div>");

            return sb.ToString();
        }

        public static string ToHtmlAll(this Track track, int index, string albumId)
        {
            return $"<li><strong>{index}. <a href=\"/Tracks/Details?albumId={albumId}&trackId={track.Id}\">{WebUtility.UrlDecode(track.Name)}</a></strong></li>";
        }

        public static string ToHtmlDetails(this Track track)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"track-details\">")
              .Append($"  <iframe src=\"{WebUtility.UrlDecode(track.Link)}\"></iframe>")
              .Append($"  <h3>Track Name: {WebUtility.UrlDecode(track.Name)}</h3>")
              .Append($"  <h3>Track Price: ${track.Price}</h3>")
              .Append("</div>");

            return sb.ToString();
        }
    }
}
