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
using static BCMLiteWebApp.Models.ViewModels.StepViewModels;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/defaultSteps")]
    [Authorize]
    public class DefaultStepsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET api/defaultSteps
        [Route("~/api/defaultPlans/{planId:int}/defaultSteps")]
        [ResponseType(typeof(StepViewModel))]
        public async Task<IHttpActionResult> GetSteps(int planId)
        {
            var steps = await GetStepsByPlanId(planId);

            if (steps == null)
            {
                return NotFound();
            }

            return Ok(steps);
        }

        // GET: api/defaultSteps/1/details
        [ResponseType(typeof(StepViewModel))]
        [Route("~/api/defaultSteps/{id:int}/details")]
        public async Task<IHttpActionResult> GetStep(int id)
        {
            var step = await GetStepById(id);

            if (step == null)
            {
                return NotFound();
            }

            return Ok(step);
        }

        // POST: api/defaultSteps    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditPlan(DefaultStep step)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!StepExists(step.StepID))
            {
                db.DefaultSteps.Add(step);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(step).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                step.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StepExists(step.StepID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Step successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { step.StepID }, Message = message });
        }

        // DELETE: api/defaultSteps/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                DefaultStep steps = await db.DefaultSteps.FindAsync(i);
                if (steps == null)
                {
                    return NotFound();
                }

                db.DefaultSteps.Remove(steps);
            }
            await db.SaveChangesAsync();

            string message = "Steps deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/defaultSteps/import    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleSteps(List<DefaultStep> stepList)
        {
            var stepIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in stepList)
            {
                db.DefaultSteps.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in stepList)
            {
                stepIds.Add(data.StepID);
            }

            status = "successful";

            string message = $"Step import { status }!";

            return Ok(new PostResponseViewModel { Ids = stepIds, Message = message });
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
        private bool StepExists(int id)
        {
            return db.DefaultSteps.Count(ds => ds.StepID == id) > 0;
        }

        public async Task<List<StepViewModel>> GetStepsByPlanId(int planId)
        {
            return await (from ds in db.DefaultSteps
                          where ds.PlanID == planId
                          select new StepViewModel
                          {
                              StepID = ds.StepID,
                              Number = ds.Number,
                              Title = ds.Title,
                              Summary = ds.Summary,
                              Detail = ds.Detail,
                              DepartmentPlanID = ds.PlanID,
                              DateModified = ds.DateModified

                          }).ToListAsync();
        }

        private async Task<StepViewModel> GetStepById(int stepId)
        {
            return await (from ds in db.DefaultSteps
                          where ds.StepID == stepId
                          select new StepViewModel
                          {
                              StepID = ds.StepID,
                              Number = ds.Number,
                              Title = ds.Title,
                              Summary = ds.Summary,
                              Detail = ds.Detail,
                              DepartmentPlanID = ds.PlanID,
                              DateModified = ds.DateModified
                          }).FirstOrDefaultAsync();
        }
        #endregion
    }
}