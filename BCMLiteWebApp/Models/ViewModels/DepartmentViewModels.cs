using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class DepartmentViewModels
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? RevenueGenerating { get; set; }

        public double? Revenue { get; set; }
    }
}