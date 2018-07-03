using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class StepViewModels
    {
        public class StepViewModel
        {
            public int StepID { get; set; }

            public int DepartmentPlanID { get; set; }

            public int? Number { get; set; }

            public string Title { get; set; }

            public string Summary { get; set; }

            public string Detail { get; set; }

            public DateTime DateModified { get; set; }

        }
    }
}