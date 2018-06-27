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
    [RoutePrefix("api/plans")]
    public class PlansApiController : ApiController
    {

        private BCMContext db = new BCMContext();

        

        // POST: api/plans    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditPlan(Plan plan)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!PlanExists(plan.PlanID))
            {
                db.Plans.Add(plan);

                await db.SaveChangesAsync();
            }
          
            return Ok(new PostResponseViewModel { Ids = new List<int>() { plan.PlanID }, Message = null });
        }

        // DELETE: api/plans/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Plan plans = await db.Plans.FindAsync(i);
                if (plans == null)
                {
                    return NotFound();
                }

                db.Plans.Remove(plans);
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
            return db.Plans.Count(d => d.PlanID == id) > 0;
        }

        #endregion
    }
}