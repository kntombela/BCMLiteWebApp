using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class ApplicationViewModel
    {
        public int ApplicationID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RTO { get; set; }

        public string RPO { get; set; }

        public int? ProcessID { get; set; }
    }

    public class SkillViewModel
    {
        public int SkillID { get; set; }

        public string Description { get; set; }

        public string RTO { get; set; }

        public int? ProcessID { get; set; }
    }

    public class ThirdPartyViewModel
    {
        public int ThirdPartyID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RTO { get; set; }

        public int? ProcessID { get; set; }
    }

    public class DocumentViewModel
    {
        public int DocumentID { get; set; }

        public string Description { get; set; }

        public string RTO { get; set; }

        public int? ProcessID { get; set; }
    }

    public class EquipmentViewModel
    {
        public int EquipmentID { get; set; }

        public string Description { get; set; }

        public string RTO { get; set; }

        public int? ProcessID { get; set; }
    }
}