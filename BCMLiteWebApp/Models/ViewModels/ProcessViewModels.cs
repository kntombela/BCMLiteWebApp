using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class ProcessDetailViewModel
    {
        public int ProcessID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CriticalTimeYear { get; set; }

        public string CriticalTimeMonth { get; set; }

        public string CriticalTimeDay { get; set; }

        public string CriticalTimeComment { get; set; }

        public bool? SOP { get; set; }

        public string SOPComment { get; set; }

        public bool? SLA { get; set; }

        public string SLAComment { get; set; }

        public string RTO { get; set; }

        public string MTPD { get; set; }

        public double? MBCO { get; set; }

        public string OperationalImpact { get; set; }

        public string FinancialImpact { get; set; }

        public double? StaffCompliment { get; set; }

        public string StaffCompDesc { get; set; }

        public double? RevisedOpsLevel { get; set; }

        public string RevisedOpsLevelDesc { get; set; }

        public bool? RemoteWorking { get; set; }

        public bool? SiteDependent { get; set; }

        public string WorkAreaComment { get; set; }

        public string Location { get; set; }
    }

    public class ProcessSummaryViewModel
    {
        public int ProcessID { get; set; }

        public string Name { get; set; }

        public string RTO { get; set; }

        public DateTime DateModified { get; set; }

        public int? DepartmentID { get; set; }
    }
}