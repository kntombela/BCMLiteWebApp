using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    [Authorize]
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

        private async Task<List<BasicUserViewModel>> GetUsersByOrganisationId(int organisationId)
        {
            //return await db.Users
            //    .Include(u => u.Organisations.Where(o => o.OrganisationID == organisationId))
            //    .Select(u => new BasicUserViewModel
            //    {
            //        Name = u.Name,
            //        Surname = u.Surname,
            //        Designation = u.Designation,
            //        AuthorityToInvoke = u.AuthorityToInvoke,
            //        MediaSpokesPerson = u.MediaSpokesPerson
            //    }).ToListAsync();

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

    }
}