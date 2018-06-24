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
    [RoutePrefix("api/defaultPlans")]
    [Authorize]
    public class DefaultPlansApiController : ApiController
    {

        private BCMContext db = new BCMContext();

        // GET api/defaultPlans
        [Route("")]
        [ResponseType(typeof(PlanViewModel))]
        public async Task<IHttpActionResult> GetAllDefaultPlans()
        {
            var plans = await GetPlans();

            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);
        }

        // GET: api/defaultPlans/1/details
        [ResponseType(typeof(PlanViewModel))]
        [Route("~/api/defaultPlans/{id:int}/details")]
        public async Task<IHttpActionResult> GetPlan(int id)
        {
            var plan = await GetPlanById(id);

            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        // POST: api/defaultPlans    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditPlan(DefaultPlan plan)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!PlanExists(plan.PlanID))
            {
                db.DefaultPlans.Add(plan);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(plan).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                plan.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.PlanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Plan successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { plan.PlanID }, Message = message });
        }

        // DELETE: api/defaultPlans/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                DefaultPlan plans = await db.DefaultPlans.FindAsync(i);
                if (plans == null)
                {
                    return NotFound();
                }

                db.DefaultPlans.Remove(plans);
            }
            await db.SaveChangesAsync();

            string message = "Plans deleted successfully!";

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
        private bool PlanExists(int id)
        {
            return db.DefaultPlans.Count(d => d.PlanID == id) > 0;
        }

        private async Task<List<PlanViewModel>> GetPlans()
        {
            return await (from dp in db.DefaultPlans
                          select new PlanViewModel
                          {
                              ID = dp.PlanID,
                              Name = dp.Name,
                              Description = dp.Description,
                              Type = dp.Type
                          }).ToListAsync();
        }

        private async Task<PlanViewModel> GetPlanById(int planId)
        {
            return await (from dp in db.DefaultPlans
                          where dp.PlanID == planId
                          select new PlanViewModel
                          {
                              ID = dp.PlanID,
                              Name = dp.Name,
                              Description = dp.Description,
                              Type = dp.Type
                          }).FirstOrDefaultAsync();
        }
        #endregion
    }
}