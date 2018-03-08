namespace BCMLiteWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bcp.Plan")]
    public partial class Plan
    {
        
        public int PlanID { get; set; }

        [StringLength(6)]
        public string PlanAbbreviation { get; set; }

        [Required]
        [StringLength(53)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(11)]
        public string Type { get; set; }

        public virtual ICollection<DepartmentPlan> DepartmentPlans { get; set; }
    }
}
