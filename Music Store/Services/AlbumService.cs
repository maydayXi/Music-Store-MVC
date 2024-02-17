using Music_Store.Models;
using Music_Store.Repository;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.Services
{
    public class AlbumService
    {
        private readonly EFRepository<Album> _albumRepository;

        /// <summary>
        /// Constructor：Initialize repository
        /// </summary>
        public AlbumService()
        {
            _albumRepository = new EFRepository<Album>(new MusicShopEntities());
        }

        /// <summary>
        /// Get album by genre id 
        /// </summary>
        /// <param name="genreId"> Identity of genre </param>
        /// <param name="genreName"> Name of genre </param>
        /// <returns> Album data set </returns>
        public IEnumerable<VmAlbum> GetAlbumsByGenre(int genreId, string genreName)
            => _albumRepository.Reads(a => a.GenreId == genreId).Select(a => new VmAlbum()
            {
                AlbumId = a.AlbumId,
                Title = a.Title,
                Genre = new VmGenre()
                {
                    Name = genreName
                }
            });
    }
}