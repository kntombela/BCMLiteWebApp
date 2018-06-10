using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
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
            var applications = await GetApplicationsByProcess(processId);

            if (applications == null)
            {
                return NotFound();
            }

            return Ok(applications);
        }

        private async Task<List<ApplicationViewModel>> GetApplicationsByProcess(int processId)
        {
            return await db.Applications.Where(p => p.ProcessID == processId)
                                                  .Select(p => new ApplicationViewModel
                                                  {
                                                      ApplicationID = p.ApplicationID,
                                                      Name = p.Name,
                                                      Description = p.Description,
                                                      RTO = p.RTO,
                                                      RPO = p.RPO,
                                                      ProcessID = p.ProcessID
                                                  }).ToListAsync();
        }
    }
}