namespace BCMLiteWebApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bcp.DefaultSteps")]
    public partial class DefaultStep
    {
        [Key]
        public int StepID { get; set; }

        public int PlanID { get; set; }

        public int CategoryID { get; set; }

        public int? Number { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        public string Detail { get; set; }

        public virtual DefaultCategory DefaultCategory { get; set; }

        public virtual DefaultPlan DefaultPlan { get; set; }
    }
}
