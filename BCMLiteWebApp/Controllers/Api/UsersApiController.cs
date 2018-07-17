using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
using static BCMLiteWebApp.Models.ViewModels.UserViewModels;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private BCMContext db = new BCMContext();

        public UsersApiController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET api/organisations/1/users
        [Route("~/api/organisations/{organisationId:int}/users")]
        [ResponseType(typeof(BasicUserViewModel))]
        public async Task<IHttpActionResult> GetOrganisationUsers(int organisationId)
        {
            var plans = await GetUsersByOrganisationId(organisationId);

            if (plans == null)
            {
                return NotFound();
            }

            return Ok(plans);
        }

        // POST: api/users/planowner    
        [Route("planowner/{userId}/{departmentPlanId:int}")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AssignPlanOwner(string userId, int departmentPlanId)
        {
            string message = "";

            //When value is not specified for model DateTime property, the value defaults to 0001-01-01
            //which is outside of the range of SQL Server's DATETIME
            //plan.DateModified = DateTime.Now;

            //Get department plan
            var departmentPlan = await db.DepartmentPlans.FindAsync(departmentPlanId);

            //Get user
            var user = db.Users.Find(userId);

            //Attach user
            departmentPlan.Users.Add(user);
            
            //Save changes
            await db.SaveChangesAsync();
            message = "Plan owner assigned";


            return Ok(new PostResponseViewModel { Ids = new List<int>() { departmentPlan.DepartmentPlanID }, Message = message });
        }

        #region Helpers
        private async Task<List<BasicUserViewModel>> GetUsersByOrganisationId(int organisationId)
        {           
            return await db.Users
                .Where(u => u.Organisations.Any(o => o.OrganisationID == organisationId))
                .Select(u => new BasicUserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Designation = u.Designation,
                    AuthorityToInvoke = u.AuthorityToInvoke,
                    MediaSpokesPerson = u.MediaSpokesPerson
                }).ToListAsync();
        }
        #endregion
    }
}