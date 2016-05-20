using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreepedOut.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Creeped Out";

            return View("Stuff");
        }

        public ActionResult MappedOut()
        {
            ViewBag.Title = "Mapped Out";
            return View();
        }
    }
}
