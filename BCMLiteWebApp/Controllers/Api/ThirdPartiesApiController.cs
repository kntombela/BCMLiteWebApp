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
    [RoutePrefix("api/thirdparties")]
    [Authorize]
    public class ThirdPartiesApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/thirdparties
        [Route("~/api/processes/{processId:int}/thirdparties")]
        [ResponseType(typeof(ThirdPartyViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var thirdparties = await GetThirdPartiesByProcess(processId);

            if (thirdparties == null)
            {
                return NotFound();
            }

            return Ok(thirdparties);
        }

        private async Task<List<ThirdPartyViewModel>> GetThirdPartiesByProcess(int processId)
        {
            return await db.ThirdParties.Where(t => t.ProcessID == processId)
                                                  .Select(t => new ThirdPartyViewModel
                                                  {
                                                      ThirdPartyID = t.ThirdPartyID,
                                                      Description = t.Description,
                                                      RTO = t.RTO,
                                                      ProcessID = t.ProcessID
                                                  }).ToListAsync();
        }

    }
}