using Music_Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.Controllers
{
    public class StoreManagerController : Controller
    {
        private readonly StoreService _storeService;
        public StoreManagerController()
        {
            _storeService = new StoreService();
        }

        // GET: Show all album's
        public ActionResult Index()
        {
            return View(_storeService.GetAlbums());
        }
    }
}