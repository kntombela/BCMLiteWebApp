using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class DepartmentPlansController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int planId)
        {
            return View();
        }

    }
}