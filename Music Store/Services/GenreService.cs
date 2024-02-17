using Music_Store.Models;
using Music_Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_Store.Services
{

    public class GenreService
    {
        private readonly EFRepository<Genre> _genreRepository;

        /// <summary>
        /// Constructor：Initialize repository
        /// </summary>
        public GenreService()
        {
            _genreRepository = new EFRepository<Genre>(new MusicShopEntities());
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns> genre data set </returns>
        public IEnumerable<Genre> GetGenres() => _genreRepository.Reads();

        /// <summary>
        /// Get genre id by genre name
        /// </summary>
        /// <param name="name"> Name of genre </param>
        /// <returns> Genre id </returns>
        public int GetGenreIdByName(string name) => _genreRepository.Read(
            g => g.GenreName ==  name)?.GenreId ?? -1;
    }
}