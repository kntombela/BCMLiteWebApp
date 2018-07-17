using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BCMLiteWebApp.Models.ViewModels;
using System.Data.Entity.Infrastructure;

namespace BCMLiteWebApp.Controllers.API
{
    [RoutePrefix("api/departmentPlans")]
    public class DepartmentPlansApiController : ApiController
    {
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private BCMContext db = new BCMContext();

        public DepartmentPlansApiController()
        {
            //Handle "Self referencing loop detected for property" error
            //db.Configuration.ProxyCreationEnabled = false;
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET api/organisations/1/departmentPlans
        [Route("~/api/organisations/{organisationId:int}/departmentPlans")]
        [ResponseType(typeof(PlanSummaryViewModel))]
        public async Task<IHttpActionResult> GetOrganisationPlans(int organisationId)
        {
            var plans = await GetPlansByOrganisationId(organisationId);

            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);
        }

        // GET: api/departmentPlans/1/details
        [ResponseType(typeof(PlanSummaryViewModel))]
        [Route("~/api/departmentPlans/{id:int}/details")]
        public async Task<IHttpActionResult> GetPlan(int id)
        {
            var plans = await GetPlanById(id);

            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);
        }


        // POST: api/departmentPlans    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditPlan(DepartmentPlan plan)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!PlanExists(plan.DepartmentPlanID))
            {
                db.DepartmentPlans.Add(plan);
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
                    if (!PlanExists(plan.DepartmentPlanID))
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

            return Ok(new PostResponseViewModel { Ids = new List<int>() { plan.DepartmentPlanID }, Message = message });
        }

        // DELETE: api/departmentPlans/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                DepartmentPlan plans = await db.DepartmentPlans.FindAsync(i);
                if (plans == null)
                {
                    return NotFound();
                }

                db.DepartmentPlans.Remove(plans);
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
            return db.DepartmentPlans.Count(d => d.DepartmentPlanID == id) > 0;
        }

        private Boolean IsAdminUser()
        {

            if (User.Identity.IsAuthenticated)
            {
                var s = UserManager.GetRoles(User.Identity.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private async Task<List<PlanSummaryViewModel>> GetPlansByOrganisationId(int organisationId)
        {
            return await (from dp in db.DepartmentPlans
             join d in db.Departments on dp.DepartmentID equals d.DepartmentID
             join p in db.Plans on dp.PlanID equals p.PlanID
             where d.Organisation.OrganisationID == organisationId
             select new PlanSummaryViewModel
             {
                 ID = dp.DepartmentPlanID,
                 Name = p.Name,
                 Description = p.Description,
                 Type = p.Type,
                 DepartmentName = d.Name,
                 DepartmentID = d.DepartmentID,
                 Invoked = dp.DepartmentPlanInvoked,
                 DateModified = dp.DateModified
             }).ToListAsync();
        }

        private async Task<PlanSummaryViewModel> GetPlanById(int planId)
        {
            return await (from dp in db.DepartmentPlans
                          join d in db.Departments on dp.DepartmentID equals d.DepartmentID
                          join p in db.Plans on dp.PlanID equals p.PlanID
                          where dp.DepartmentPlanID == planId
                          select new PlanSummaryViewModel
                          {
                              ID = dp.DepartmentPlanID,
                              Name = p.Name,
                              Description = p.Description,
                              Type = p.Type,
                              DepartmentName = d.Name,
                              DepartmentID = d.DepartmentID,
                              DateModified = dp.DateModified
                          }).SingleOrDefaultAsync();
        }
        #endregion
    }
}