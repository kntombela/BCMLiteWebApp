using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class DepartmentsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}