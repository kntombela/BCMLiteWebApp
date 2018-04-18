using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/processes")]
    [Authorize]
    public class ProcessesApiController : ApiController
    {

        private BCMContext db = new BCMContext();

        [ResponseType(typeof(ProcessSummaryViewModel))]
        // GET: api/departments/1/processes
        [Route("~/api/departments/{departmentId:int}/processes")]
        public async Task<IHttpActionResult> GetProcesses(int departmentId)
        {
            var processes = await GetProcessSummaryByDepartmentId(departmentId);

            if (processes == null)
            {
                return NotFound();
            }

            return Ok(processes);
        }

        [ResponseType(typeof(ProcessDetailViewModel))]
        // GET: api/departments/1
        [Route("~/api/processes/{id:int}/details")]
        public async Task<IHttpActionResult> GetProcess(int id)
        {
            var processes = await GetProcessDetailById(id);

            if (processes == null)
            {
                return NotFound();
            }

            return Ok(processes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers
        private async Task<List<ProcessSummaryViewModel>> GetProcessSummaryByDepartmentId(int departmentId)
        {
            return await db.Processes.Where(p => p.DepartmentID == departmentId)
                                                  .Select(p => new ProcessSummaryViewModel
                                                  {
                                                      ProcessID = p.ProcessID,
                                                      Name = p.Name,
                                                      RTO = p.RTO,
                                                      DateModified = p.DateModified,
                                                      DepartmentID = p.DepartmentID
                                                  }).ToListAsync(); 
        }

        private async Task<ProcessSummaryViewModel> GetProcessSummaryById(int id)
        {
            return await db.Processes.Where(p => p.ProcessID == id)
                                                  .Select(p => new ProcessSummaryViewModel
                                                  {
                                                      ProcessID = p.ProcessID,
                                                      Name = p.Name,
                                                      RTO = p.RTO,
                                                      DateModified = p.DateModified,
                                                      DepartmentID = p.DepartmentID
                                                  }).FirstOrDefaultAsync();
        }

        private async Task<ProcessDetailViewModel> GetProcessDetailById(int id)
        {
            return await db.Processes.Where(p => p.ProcessID == id)
                                                  .Select(p => new ProcessDetailViewModel
                                                  {
                                                      ProcessID = p.ProcessID,
                                                      Name = p.Name,
                                                      Description = p.Description,
                                                      RTO = p.RTO,
                                                      FinancialImpact = p.FinancialImpact,
                                                      OperationalImpact = p.OperationalImpact,
                                                      CriticalTime = p.CriticalTime,
                                                      Location = p.Location,
                                                      MBCO = p.MBCO,
                                                      RemoteWorking = p.RemoteWorking,
                                                      SiteDependent = p.SiteDependent,
                                                      SLA = p.SLA,
                                                      SOP = p.SOP,
                                                      StaffCompliment = p.StaffCompliment,
                                                      StaffCompDesc = p.StaffCompDesc,
                                                      RevisedOpsLevel = p.RevisedOpsLevel,
                                                      RevisedOpsLevelDesc = p.RevisedOpsLevelDesc
                                                  }).FirstOrDefaultAsync();
        }
        #endregion
    }
}