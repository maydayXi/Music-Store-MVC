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
        // GET: Store
        public ActionResult Index()
        {

            return View(new List<VmGenre>()
            {
                new VmGenre() { Name = "Disco" },
                new VmGenre() { Name = "Jazz" },
                new VmGenre() { Name = "Rock" },
            });
        }


        public ActionResult Browse(string genre)
        {
            return View(new VmGenre()
            {
                Name = genre,
            });
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