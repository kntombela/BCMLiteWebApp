using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models.ViewModels;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Linq;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/organisations")]
    [Authorize]
    public class OrganisationsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET api/organisations
        [Route("")]
        [ResponseType(typeof(OrganisationViewModel))]
        public async Task<IHttpActionResult> GetOrganisations()
        {
            var organisations = await db.Organisations.Select(o => new OrganisationViewModel
            {
                OrganisationID = o.OrganisationID,
                Name = o.Name,
                Type = o.Type,
                Industry = o.Industry,
                NumberOfPlans = 0
            }).ToListAsync();

            if (organisations == null)
            {
                return NotFound();
            }

            return Ok(organisations);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}