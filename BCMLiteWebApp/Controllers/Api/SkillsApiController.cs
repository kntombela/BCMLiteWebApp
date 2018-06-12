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
    [RoutePrefix("api/skills")]
    [Authorize]
    public class SkillsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/skills
        [Route("~/api/processes/{processId:int}/skills")]
        [ResponseType(typeof(SkillViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var skills = await GetSkillsByProcess(processId);

            if (skills == null)
            {
                return NotFound();
            }

            return Ok(skills);
        }

        private async Task<List<SkillViewModel>> GetSkillsByProcess(int processId)
        {
            return await db.Skills.Where(s => s.ProcessID == processId)
                                                  .Select(s => new SkillViewModel
                                                  {
                                                      SkillID = s.SkillID,                                                          
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID
                                                  }).ToListAsync();
        }

    }
}