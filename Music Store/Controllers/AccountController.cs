using Music_Store.Services;
using Music_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace Music_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("LogOn", "Account");
        }

        /// <summary>
        /// Log on 
        /// </summary>
        /// <returns> Log on page </returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// User register
        /// </summary>
        /// <param name="vmLogon"> User information </param>
        /// <returns> Login page </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VmLogon vmLogon)
        {
            if(ModelState.IsValid)
                _accountService.RegisterUser(vmLogon);

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Log in 
        /// </summary>
        /// <returns> Login page </returns>
        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Exclude = "IsAdmin")]VmLogon vmLogon)
        {
            if(ModelState.IsValid)
            {
                HttpContext.User = _accountService.GetUserClaims(vmLogon);

                TempData["IsAdmin"] = HttpContext.User.IsInRole("Administrator");

                return HttpContext.User.IsInRole("Administrator")
                    ? RedirectToAction("Index", "StoreManager")
                    : RedirectToAction("Index", "Store");
            }

            return View(vmLogon);
        }

        /// <summary>
        /// Log out
        /// </summary>
        /// <returns> Home page </returns>
        public ActionResult LogOff()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}