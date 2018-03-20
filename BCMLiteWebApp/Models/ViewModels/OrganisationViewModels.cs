using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class OrganisationViewModel
    {
        public int OrganisationID { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Type { get; set; }
        public int NumberOfPlans { get; set; }
    }
}