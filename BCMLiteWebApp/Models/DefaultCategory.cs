namespace BCMLiteWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bcp.DefaultCategories")]
    public partial class DefaultCategory
    {

        [Key]
        public int CategoryID { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Description { get; set; }

        public virtual ICollection<DefaultStep> DefaultSteps { get; set; }
    }
}
