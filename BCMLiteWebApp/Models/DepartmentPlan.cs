namespace BCMLiteWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bcp.DepartmentPlan")]
    public partial class DepartmentPlan
    {

        public DepartmentPlan()
        {
            Users = new HashSet<ApplicationUser>();
        }

        public int DepartmentPlanID { get; set; }

        public int DepartmentID { get; set; }

        public int PlanID { get; set; }

        public bool? DepartmentPlanInvoked { get; set; }

        public virtual Department Department { get; set; }

        public virtual Plan Plan { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
