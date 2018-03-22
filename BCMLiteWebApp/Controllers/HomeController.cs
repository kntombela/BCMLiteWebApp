using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            //if user is admin present a drop down list to select available organisations
            //ViewBag.IsAdmin = false;
            //var model = new OrganisationDropDownViewModel();

            //if (IsAdminUser())
            //{
            //    ViewBag.IsAdmin = true;
            //}

            return View();
        }

        //#region Helpers
        //public Boolean IsAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var s = UserManager.GetRoles(User.Identity.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}
        //#endregion
    }
}