using BCMLiteWebApp.Models;
using System;
using System.Collections.Generic;

namespace BCMLiteWebApp.ViewModels
{

    public class PlanSummaryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public bool? Invoked { get; set; }

        public string DepartmentName { get; set; }

        public int DepartmentID { get; set; }

        public DateTime DateModified { get; set; }

    }

    public class PlanViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Abbreviation { get; set; }

        public DateTime DateModified { get; set; }

    }

}