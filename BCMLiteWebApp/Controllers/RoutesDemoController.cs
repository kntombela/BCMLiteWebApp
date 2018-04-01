using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class RoutesDemoController : Controller
    {
        public ActionResult One()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult Two()
        {
            return View();
        }   

        public ActionResult Three()
        {
            return View();
        }
    }
}