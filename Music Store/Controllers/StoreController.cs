using Music_Store.Services;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.Controllers
{
    public class StoreController : Controller
    {
        private readonly GenreService genreService;
        private readonly AlbumService albumService;

        public StoreController()
        {
            genreService = new GenreService();
            albumService = new AlbumService();
        }

        // GET: Show all genre
        public ActionResult Index()
        {
            return View(genreService.GetGenres().Select(g => new VmGenre()
            {
                Name = g.GenreName
            }));
        }

        /// <summary>
        /// Show albums
        /// </summary>
        /// <param name="genre"> Genre's name </param>
        /// <returns> Genre of albums </returns>
        public ActionResult Browse(string genre)
        {
            if (string.IsNullOrEmpty(genre)) 
                return RedirectToAction("Error", "Home");

            ViewBag.Genre = genre;

            int genreId = genreService.GetGenreIdByName(genre);
            if(genreId <= 0) 
                return RedirectToAction("Error", "Home");

            return View(albumService.GetAlbumsByGenre(genreId, genre));
        }

        /// <summary>
        /// Get /Store/Details/{id}
        /// </summary>
        /// <param name="id"> Album identity </param>
        /// <returns> Album detail </returns>
        public ActionResult Details(int? id)
        { 
            return View(new VmAlbum()
            {
                Title = $"Album {id}"
            });
        }
    }
}