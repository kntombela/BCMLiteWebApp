namespace BCMLiteWebApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bia.ThirdParty")]
    public partial class ThirdParty
    {
        public int ThirdPartyID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string RTO { get; set; }

        public int RTOValue { get; set; }

        public int? ProcessID { get; set; }

        public virtual Process Process { get; set; }
    }
}
