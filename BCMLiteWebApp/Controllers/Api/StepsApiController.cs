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
    [RoutePrefix("api/steps")]
    public class StepsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET api/steps
        [Route("~/api/departmentPlans/{departmentPlanId:int}/steps")]
        [ResponseType(typeof(StepViewModel))]
        public async Task<IHttpActionResult> GetSteps(int departmentPlanId)
        {
            var steps = await GetStepsByPlanId(departmentPlanId);

            if (steps == null)
            {
                return NotFound();
            }

            return Ok(steps);
        }

        // GET: api/steps/1/details
        [ResponseType(typeof(StepViewModel))]
        [Route("{id:int}/details")]
        public async Task<IHttpActionResult> GetStep(int id)
        {
            var step = await GetStepById(id);

            if (step == null)
            {
                return NotFound();
            }

            return Ok(step);
        }

        // POST: api/steps/copyDefaultSteps/3/3   
        [Route("copyDefaultSteps/{planId:int}/{departmentPlanId:int}")]
        [HttpPost]
        [ResponseType(typeof(StepViewModel))]
        public async Task<IHttpActionResult> CopyDefaultSteps(int planId, int departmentPlanId)
        {         
            if (!DepartmentPlanExists(departmentPlanId))
            {    
                //Copy default steps to steps and associate with department planId
                var stepCopy = await db.DefaultSteps.Where(s => s.PlanID == planId).ToListAsync();

                foreach (var data in stepCopy)
                {
                    Step step = new Step
                    {
                        DepartmentPlanID = departmentPlanId,
                        Number = data.Number,
                        Title = data.Title,
                        Summary = data.Summary,
                        Detail = data.Detail
                    };

                    db.Steps.Add(step);
                }

                await db.SaveChangesAsync();             
            }

            //Get steps
            var steps = await GetStepsByPlanId(departmentPlanId);

            return Ok(steps);
        }


        // POST: api/steps    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditStep(Step step)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!StepExists(step.StepID))
            {
                db.Steps.Add(step);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(step).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                //step.DateModified = DateTime.Now;

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

        // DELETE: api/steps/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Step steps = await db.Steps.FindAsync(i);
                if (steps == null)
                {
                    return NotFound();
                }

                db.Steps.Remove(steps);
            }
            await db.SaveChangesAsync();

            string message = "Steps deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/steps/import    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleSteps(List<Step> stepList)
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
                db.Steps.Add(data);
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
            return db.Steps.Count(ds => ds.StepID == id) > 0;
        }

        private bool DepartmentPlanExists(int id)
        {
            return db.Steps.Count(ds => ds.DepartmentPlanID == id) > 0;
        }

        private async Task<List<StepViewModel>> GetStepsByPlanId(int departmentPlanId)
        {
            return await (from s in db.Steps
                          where s.DepartmentPlanID == departmentPlanId
                          select new StepViewModel
                          {
                              StepID = s.StepID,
                              Number = s.Number,
                              Title = s.Title,
                              Summary = s.Summary,
                              Detail = s.Detail,
                              DepartmentPlanID = s.DepartmentPlanID
                          }).ToListAsync();
        }

        private async Task<StepViewModel> GetStepById(int stepId)
        {
            return await (from s in db.Steps
                          where s.StepID == stepId
                          select new StepViewModel
                          {
                              StepID = s.StepID,
                              Number = s.Number,
                              Title = s.Title,
                              Summary = s.Summary,
                              Detail = s.Detail,
                              DepartmentPlanID = s.DepartmentPlanID
                          }).FirstOrDefaultAsync();
        }
        #endregion
    }
}