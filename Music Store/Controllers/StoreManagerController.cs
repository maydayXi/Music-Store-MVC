using Music_Store.Services;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.Controllers
{
    public class StoreManagerController : Controller
    {
        private readonly RedirectToRouteResult ErrorPageAction;
        private readonly StoreService _storeService;

        public StoreManagerController()
        {
            ErrorPageAction = RedirectToAction("Error", "Home");
            _storeService = new StoreService();
        }

        // GET: Show all album's
        public ActionResult Index()
        {
            return View(_storeService.GetAlbums());
        }

        /// <summary>
        /// Get：StoreManager/Create
        /// </summary>
        /// <returns> Create page </returns>
        public ActionResult Create()
        {
            VmAlbumEdit vmAlbumEdit = _storeService.GetDefaultAlbum();
            vmAlbumEdit.SelectArtists.First().Selected = true;
            vmAlbumEdit.SelectGenres.First().Selected = true;
            return View(vmAlbumEdit);
        }

        /// <summary>
        /// Post：Create new album
        /// </summary>
        /// <param name="vmAlbumEdit"> New album data user input </param>
        /// <returns> Album list page </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VmAlbumEdit vmAlbumEdit)
        {
            _storeService.CreateNewAlbum(vmAlbumEdit);
            return RedirectToAction("Index", "StoreManager");
        }

        /// <summary>
        /// Get：Show Album information
        /// </summary>
        /// <param name="id"> Album identity </param>
        /// <returns> Album edit </returns>
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return ErrorPageAction;
            }
            else
            {
                VmAlbumEdit album = _storeService.GetAlbumById(id);
                return album == null ? ErrorPageAction : (ActionResult)View(album);
            }
        }

        /// <summary>
        /// Update album data
        /// </summary>
        /// <param name="vmAlbumEdit"> New album data from user input </param>
        /// <returns> Album list </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VmAlbumEdit vmAlbumEdit)
        {
            try
            {
                _storeService.UpdateAlbum(vmAlbumEdit);
                return RedirectToAction("Index", "StoreManager");
            }
            catch (Exception)
            {
                return ErrorPageAction;
            }
        }
    }
}