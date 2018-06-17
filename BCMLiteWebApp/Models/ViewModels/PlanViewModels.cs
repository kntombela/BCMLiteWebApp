using BCMLiteWebApp.Models;
using System.Collections.Generic;

namespace BCMLiteWebApp.ViewModels
{

    public class PlanViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? Invoked { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentID { get; set; }

    }

    public class PlanStepsViewModel
    {
        public int DepartmentPlanID { get; set; }
        public int? Number { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
    }

    public class PlanTemplateViewModel
    {
        public int PlanID { get; set; }

        public string PlanAbbreviation { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}