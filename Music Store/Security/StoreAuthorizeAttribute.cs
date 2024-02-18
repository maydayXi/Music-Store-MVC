using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_Store.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class StoreAuthorizeAttribute : AuthorizeAttribute
    {
        private bool IsAdmin {  get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IsAdmin = filterContext.Controller.TempData["IsAdmin"] as bool? ?? false;

            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return IsAdmin;
        }

        /// <summary>
        /// If authorize failed redirect to login page
        /// </summary>
        /// <param name="filterContext"> Filter context </param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    {"controller", "Account" },
                    {"action", "Login" }
                }
            );
        }
    }
}