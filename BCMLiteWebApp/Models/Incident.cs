using System;
using System.ComponentModel.DataAnnotations;

namespace BCMLiteWebApp.Models
{
    public enum IncidentType
    {
        [Display(Name = "Building & Facility")]
        Facility,
        [Display(Name = "Health & Safety")]
        HealthAndSafety,
        Security,
        [Display(Name = "Information Technology")]
        InformationTechnology,
        Other
}

    public class Incident
    {
        public int IncidentID { get; set; }

        public IncidentType? Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int? OrganisationID { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}