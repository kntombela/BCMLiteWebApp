namespace BCMLiteWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Department")]
    public partial class Department
    {

        public int DepartmentID { get; set; }

        [StringLength(28)]
        public string Name { get; set; }

        [StringLength(105)]
        public string Description { get; set; }

        public bool? RevenueGenerating { get; set; }

        [StringLength(8)]
        public string Revenue { get; set; }

        public int? OrganisationID { get; set; }
      
        public virtual ICollection<DepartmentPlan> DepartmentPlans { get; set; }
      
        public virtual ICollection<Process> Processes { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
