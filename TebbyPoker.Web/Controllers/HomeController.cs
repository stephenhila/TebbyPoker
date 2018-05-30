using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TebbyPoker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About the author:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hi. If you find that the output of this take-home exam has been being good enough, then please contact me through the following:";

            return View();
        }
    }
}