using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using BCMLiteWebApp.ViewModels;
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
    [RoutePrefix("api/planTemplates")]
    [Authorize]
    public class PlanTemplatesApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/planTemplates
        [ResponseType(typeof(Plan))]
        [Route("")]
        public async Task<IHttpActionResult> GetTemplates()
        {
            var plans = await GetPlanTemplates();

            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);
        }

        // GET: api/planTemplates/1/details
        [ResponseType(typeof(PlanTemplateViewModel))]
        [Route("~/api/planTemplates/{id:int}/details")]
        public async Task<IHttpActionResult> GetApplication(int id)
        {
            var planTemplate = await GetPlanTemplateById(id);

            if (planTemplate == null)
            {
                return NotFound();
            }

            return Ok(planTemplate);
        }

        // POST: api/plans    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditPlanTemplate(Plan plan)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!PlanTemplateExists(plan.PlanID))
            {
                db.Plans.Add(plan);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(plan).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                //plan.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTemplateExists(plan.PlanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Plan template successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { plan.PlanID }, Message = message });
        }



        #region Helpers
        private bool PlanTemplateExists(int id)
        {
            return db.Plans.Count(p => p.PlanID == id) > 0;
        }

        private async Task<List<PlanTemplateViewModel>> GetPlanTemplates()
        {
            return await (from p in db.Plans
                          select new PlanTemplateViewModel
                          {
                              PlanID = p.PlanID,
                              Name = p.Name,
                              Description = p.Description,
                              PlanAbbreviation = p.PlanAbbreviation,
                              Type = p.Type
                          }).ToListAsync();
        }

        private async Task<PlanTemplateViewModel> GetPlanTemplateById(int planTemplateId)
        {
            return await (from p in db.Plans
                          where p.PlanID == planTemplateId
                          select new PlanTemplateViewModel
                          {
                              PlanID = p.PlanID,
                              Name = p.Name,
                              Description = p.Description,
                              PlanAbbreviation = p.PlanAbbreviation,
                              Type = p.Type
                          }).FirstOrDefaultAsync();
        }
        #endregion
    }
}