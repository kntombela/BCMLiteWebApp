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

        // GET: api/skills/1/details
        [ResponseType(typeof(SkillViewModel))]
        [Route("~/api/skills/{id:int}/details")]
        public async Task<IHttpActionResult> GetSkill(int id)
        {
            var skills = await GetSkillById(id);

            if (skills == null)
            {
                return NotFound();
            }

            return Ok(skills);
        }

        // POST: api/skills    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditSkill(Skill skill)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!SkillExists(skill.SkillID))
            {
                db.Skills.Add(skill);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(skill).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                skill.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.SkillID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Skill successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { skill.SkillID }, Message = message });
        }

        // DELETE: api/skills/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Skill skill = await db.Skills.FindAsync(i);
                if (skill == null)
                {
                    return NotFound();
                }

                db.Skills.Remove(skill);
            }
            await db.SaveChangesAsync();

            string message = "Skills deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/skills    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleSkills(List<Skill> skillList)
        {
            var skillIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in skillList)
            {
                db.Skills.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in skillList)
            {
                skillIds.Add(data.SkillID);
            }

            status = "successful";

            string message = $"Skill import { status }!";

            return Ok(new PostResponseViewModel { Ids = skillIds, Message = message });
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
        private bool SkillExists(int id)
        {
            return db.Skills.Count(s => s.SkillID == id) > 0;
        }

        private async Task<List<SkillViewModel>> GetSkillsByProcess(int processId)
        {
            return await db.Skills.Where(s => s.ProcessID == processId)
                                                  .Select(s => new SkillViewModel
                                                  {
                                                      SkillID = s.SkillID,                                                          
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).ToListAsync();
        }

        private async Task<SkillViewModel> GetSkillById(int id)
        {
            return await db.Skills.Where(s => s.SkillID == id)
                                                  .Select(s => new SkillViewModel
                                                  {
                                                      SkillID = s.SkillID,       
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).FirstOrDefaultAsync();
        }

        #endregion
    }
}