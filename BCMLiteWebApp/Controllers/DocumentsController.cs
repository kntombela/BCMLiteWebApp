using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class DocumentsController : Controller
    {
        public ActionResult Index(int processId)
        {
            return View();
        }
    }
}