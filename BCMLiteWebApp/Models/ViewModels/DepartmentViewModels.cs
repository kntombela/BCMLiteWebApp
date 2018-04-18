using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? RevenueGenerating { get; set; }

        public double? Revenue { get; set; }

        public DateTime DateModified { get; set; }

        public int OrganisationID { get; set; }
    }

}