using Entities.Helper;
using Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebRegister.Filters.MVC
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeUserAttribute"/> class.
        /// </summary>
        public AuthorizeUserAttribute()
        {
            this.AuthorizationService = (IAuthorizationService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAuthorizationService));
        }

        /// <summary>
        /// Gets or sets the Authorization Service.
        /// </summary>
        private IAuthorizationService AuthorizationService { get; set; }
        /// <summary>
        /// Called when a process requests authorization. Performs authorization by calling the <see cref="AuthorizationService"/>. 
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="AuthorizeUserAttribute"/></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var action = filterContext.ActionDescriptor;
            if (action.IsDefined(typeof(WinAuthenticateAttribute), true))
                return;

            base.OnAuthorization(filterContext);

            if(filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (ConfigReader.GetConfigStringValue("EnableSSO").ToString() == "1")
            {
                System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = System.IdentityModel.Claims.ClaimTypes.NameIdentifier;
            }
            string loggedOnUser = filterContext.HttpContext.User.Identity.Name;
            var controller = filterContext.Controller as Controller;

            //check if the user has the valid profile
            if(!this.AuthorizationService.HasUserProfile(loggedOnUser))
            {
                var returnUrl = string.Empty;
                if (controller != null && controller.Request != null && controller.Request.Url != null)
                {
                    returnUrl = controller.Request.Url.AbsoluteUri;
                }
                filterContext.HttpContext.Items["key"] = returnUrl;
                if (controller.Request.Url.ToString().Contains("Home"))
                {
                    string redirectURL = String.Format(ConfigReader.GetConfigStringValue("HomeURL"), Convert.ToString(filterContext.RouteData.Values["id"]));
                    filterContext.Result = new RedirectResult(redirectURL);
                }
                else
                {
                    //filterContext.Result = new RedirectResult("~/Windows/Pages/UserProfile.aspx?ret=" + returnUrl);
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "LogIn", controller = "Home" }));
                    //filterContext.Result = new RedirectResult("~/Pages/Profile");
                }
                return;
            }
        }
  }
}