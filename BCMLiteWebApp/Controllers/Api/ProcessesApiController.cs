using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // GET: api/departments/1/processes
        [ResponseType(typeof(ProcessSummaryViewModel))]
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
        // GET: api/processes/1
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

        // POST: api/processes    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditProcess(Process process)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!ProcessExists(process.ProcessID))
            {
                db.Processes.Add(process);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(process).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                process.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessExists(process.ProcessID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Process successfully { status }!";

            return Ok(new PostResponseViewModel { Id = process.ProcessID, Message = message });
        }

        // DELETE: api/processes/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Process process = await db.Processes.FindAsync(i);
                if (process == null)
                {
                    return NotFound();
                }

                db.Processes.Remove(process);
            }
            await db.SaveChangesAsync();

            string message = "Processes deleted successfully!";

            return Ok(new PostResponseViewModel { Id = null, Message = message });
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
        private bool ProcessExists(int id)
        {
            return db.Processes.Count(e => e.ProcessID == id) > 0;
        }

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
                                                      MTPD = p.MTPD,
                                                      FinancialImpact = p.FinancialImpact,
                                                      OperationalImpact = p.OperationalImpact,
                                                      CriticalTimeYear = p.CriticalTimeYear,
                                                      CriticalTimeMonth = p.CriticalTimeMonth,
                                                      CriticalTimeDay = p.CriticalTimeDay,
                                                      CriticalTimeComment = p.CriticalTimeComment,
                                                      Location = p.Location,
                                                      MBCO = p.MBCO, 
                                                      RemoteWorking = p.RemoteWorking,
                                                      SiteDependent = p.SiteDependent,
                                                      WorkAreaComment = p.WorkAreaComment,
                                                      SLA = p.SLA,
                                                      SLAComment = p.SLAComment,
                                                      SOP = p.SOP,
                                                      SOPComment = p.SOPComment,
                                                      StaffCompliment = p.StaffCompliment,
                                                      StaffCompDesc = p.StaffCompDesc,
                                                      RevisedOpsLevel = p.RevisedOpsLevel,
                                                      RevisedOpsLevelDesc = p.RevisedOpsLevelDesc
                                                  }).FirstOrDefaultAsync();
        }
        #endregion
    }
}