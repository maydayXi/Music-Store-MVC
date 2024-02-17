using Music_Store.Models;
using Music_Store.Repository;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.Services
{
    public class StoreService
    {
        private readonly EFRepository<Album> _albumRepository;
        private readonly EFRepository<Artist> _artistRepository;
        private readonly EFRepository<Genre> _genreRepository;

        /// <summary>
        /// Constructor：Initialize repository
        /// </summary>
        public StoreService()
        {
            MusicShopEntities _context = new MusicShopEntities();
            _albumRepository = new EFRepository<Album>(_context);
            _artistRepository = new EFRepository<Artist>(_context);
            _genreRepository = new EFRepository<Genre>(_context);
        }

        /// <summary>
        /// Get album list for manager page
        /// </summary>
        /// <returns> album data set </returns>
        public IEnumerable<VmAlbumManager> GetAlbums() => _albumRepository.Reads()
            .Join(_artistRepository.Reads(), album => album.ArtistId, artist => artist.ArtistId,
            (album, artist) => new
            {
                album.AlbumId,
                album.GenreId,
                artist.ArtistName,
                album.Title,
                album.Price
            })
            .Join(_genreRepository.Reads(), src => src.GenreId, g => g.GenreId,
            (src, g) => new VmAlbumManager()
            {
                AlbumId = src.AlbumId,
                Genre = new VmGenre() { Name = g.GenreName },
                Artist = new VmArtist() { Name = src.ArtistName },
                Title = src.Title,
                AlbumPrice = src.Price ?? 0
            });
    }
}