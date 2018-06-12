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
    [RoutePrefix("api/applications")]
    [Authorize]
    public class ApplicationsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/applications
        [Route("~/api/processes/{processId:int}/applications")]
        [ResponseType(typeof(ApplicationViewModel))]
        public async Task<IHttpActionResult> GetApplications(int processId)
        {
            var applications = await GetApplicationsByProcessId(processId);

            if (applications == null)
            {
                return NotFound();
            }

            return Ok(applications);
        }

        // GET: api/applications/1/details
        [ResponseType(typeof(ApplicationViewModel))]
        [Route("~/api/applications/{id:int}/details")]
        public async Task<IHttpActionResult> GetApplication(int id)
        {
            var applications = await GetApplicationById(id);

            if (applications == null)
            {
                return NotFound();
            }

            return Ok(applications);
        }

        // POST: api/applications    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditApplication(Application application)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!ApplicationExists(application.ApplicationID))
            {
                db.Applications.Add(application);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(application).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                application.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Application successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { application.ApplicationID }, Message = message });
        }

        // DELETE: api/applications/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Application application = await db.Applications.FindAsync(i);
                if (application == null)
                {
                    return NotFound();
                }

                db.Applications.Remove(application);
            }
            await db.SaveChangesAsync();

            string message = "Applications deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
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
        private bool ApplicationExists(int id)
        {
            return db.Applications.Count(a => a.ApplicationID == id) > 0;
        }

        private async Task<List<ApplicationViewModel>> GetApplicationsByProcessId(int processId)
        {
            return await db.Applications.Where(p => p.ProcessID == processId)
                                                  .Select(p => new ApplicationViewModel
                                                  {
                                                      ApplicationID = p.ApplicationID,
                                                      Name = p.Name,
                                                      Description = p.Description,
                                                      RTO = p.RTO,
                                                      RPO = p.RPO,
                                                      ProcessID = p.ProcessID,
                                                      DateModified = p.DateModified
                                                  }).ToListAsync();
        }

        private async Task<ApplicationViewModel> GetApplicationById(int id)
        {
            return await db.Applications.Where(a => a.ApplicationID == id)
                                                  .Select(a => new ApplicationViewModel
                                                  {
                                                      ApplicationID = a.ApplicationID,
                                                      Name = a.Name,
                                                      Description = a.Description,
                                                      RTO = a.RTO,
                                                      ProcessID = a.ProcessID,
                                                      DateModified = a.DateModified
                                                  }).FirstOrDefaultAsync();
        }

        #endregion
    }
}