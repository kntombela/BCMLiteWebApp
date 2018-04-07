namespace BCMLiteWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Department")]
    public partial class Department
    {

        public int DepartmentID { get; set; }

        [StringLength(28)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? RevenueGenerating { get; set; }

        public double? Revenue { get; set; }

        public int OrganisationID { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; } = DateTime.Now;

        public virtual ICollection<DepartmentPlan> DepartmentPlans { get; set; }
      
        public virtual ICollection<Process> Processes { get; set; }

        public virtual Organisation Organisation { get; set; }


    }
}
