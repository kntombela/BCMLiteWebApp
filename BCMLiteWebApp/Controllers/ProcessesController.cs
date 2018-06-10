using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class ProcessesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int processId)
        {
            return View();
        }

        public ActionResult Details(int processId)
        {
            return View();
        }

    }
}