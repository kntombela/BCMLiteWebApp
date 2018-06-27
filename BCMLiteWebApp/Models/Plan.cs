namespace BCMLiteWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bcp.Plan")]
    public partial class Plan
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanID { get; set; }

        [StringLength(6)]
        public string Abbreviation { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        public virtual ICollection<DepartmentPlan> DepartmentPlans { get; set; }
    }
}
