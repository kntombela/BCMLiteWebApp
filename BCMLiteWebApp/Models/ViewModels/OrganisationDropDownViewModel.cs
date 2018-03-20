using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class OrganisationDropDownViewModel
    {
        public int Selected { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }
    }
}