using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRegister.Filters.MVC;

namespace WebRegister.Controllers
{
   // [AuthorizeUserAttribute]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}