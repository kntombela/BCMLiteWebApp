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

namespace BCMLiteWebApp.Controllers.API
{
    [RoutePrefix("api/plans")]
    [Authorize]
    public class PlansApiController : ApiController
    {
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private BCMContext db = new BCMContext();

        public PlansApiController()
        {
            //Handle "Self referencing loop detected for property" error
            //db.Configuration.ProxyCreationEnabled = false;
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET api/organisations/1/plans
        [Route("~/api/organisations/{organisationId:int}/plans")]
        [ResponseType(typeof(PlanViewModel))]
        public async Task<IHttpActionResult> GetPlansByOrganisation(int organisationId)
        {
            var plans = new List<PlanViewModel>();

            //If current user is not admin, show only plans from the user's organisation
            if (!IsAdminUser())
            {
                var currentUser = User.Identity.GetUserId();

                var id = db.Organisations.Where(o => o.Users.Any(u => u.Id == currentUser)).Single().OrganisationID;
                plans = await GetOrganisationPlans(id);             
            }
            else
            {
                plans = await GetOrganisationPlans(organisationId);
            }


            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);

        }

        // GET api/Plan/Steps?planId=1
        [ResponseType(typeof(PlanStepsViewModel))]
        [Route("Steps")]
        public async Task<IHttpActionResult> GetPlanSteps(int planId)
        {

            var steps = await (from dp in db.DepartmentPlans
                               join d in db.Departments on dp.DepartmentID equals d.DepartmentID
                               join s in db.Steps on dp.DepartmentPlanID equals s.DepartmentPlanID
                               join p in db.Plans on dp.PlanID equals p.PlanID
                               where dp.DepartmentPlanID == planId
                               select new PlanStepsViewModel
                               {
                                   DepartmentPlanID = dp.DepartmentPlanID,
                                   Number = s.Number,
                                   Title = s.Title,
                                   Summary = s.Summary,
                                   Detail = s.Detail,

                               }).ToListAsync();

            if (steps == null)
            {
                return NotFound();
            }

            return Ok(steps);

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

        private async Task<List<PlanViewModel>> GetOrganisationPlans(int organisationId)
        {
            return await (from dp in db.DepartmentPlans
             join d in db.Departments on dp.DepartmentID equals d.DepartmentID
             join p in db.Plans on dp.PlanID equals p.PlanID
             where d.Organisation.OrganisationID == organisationId
             select new PlanViewModel
             {
                 ID = dp.DepartmentPlanID,
                 Name = p.Name,
                 Description = p.Description,
                 Type = p.Type,
                 DepartmentName = d.Name,
                 DepartmentID = d.DepartmentID
             }).ToListAsync();
        }
        #endregion
    }
}