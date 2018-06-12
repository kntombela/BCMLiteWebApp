namespace BCMLiteWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bia.Application")]
    public partial class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string RTO { get; set; }

        public int RTOValue { get; set; }

        [StringLength(255)]
        public string RPO { get; set; }

        public int RPOValue { get; set; }

        public int? ProcessID { get; set; }

        public virtual Process Process { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
