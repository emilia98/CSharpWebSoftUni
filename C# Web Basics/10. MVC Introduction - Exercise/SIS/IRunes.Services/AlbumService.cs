using System.Collections.Generic;
using System.Linq;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly RunesDbContext context;

        public AlbumService()
        {
            this.context = new RunesDbContext();
        }

        public bool AddTrackToAlbum(string albumId, Track trackForDb)
        {
            Album albumFromDb = this.GetAlbumId(albumId);

            if(albumFromDb == null)
            {
                return false;
            }

            // TODO: Include(a => a.Tracks)
            albumFromDb.Tracks.Add(trackForDb);
            albumFromDb.Price = (albumFromDb.Tracks.Select(track => track.Price).Sum() * 0.87m);

            this.context.Update(albumFromDb);
            this.context.SaveChanges();

            return true;
        }

        public Album CreateAlbum(Album album)
        {
            album = context.Albums.Add(album).Entity;
            context.SaveChanges();
            return album;
        }

        public Album GetAlbumId(string id)
        {
            return context.Albums
                          .Include(album => album.Tracks)
                          .SingleOrDefault(album => album.Id == id);
        }

        public ICollection<Album> GetAllAlbums()
        {
            return context.Albums.ToList();
        }
    }
}
