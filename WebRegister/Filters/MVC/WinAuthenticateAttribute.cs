using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRegister.Filters.MVC
{
    public class WinAuthenticateAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// An authorize attribute for User profile load page.
        /// </summary>
        public WinAuthenticateAttribute()
        {
        }

        /// <summary>
        /// Checks if user is windows authenticated.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                return true;
            else
                return base.AuthorizeCore(httpContext);
        }
    }
}