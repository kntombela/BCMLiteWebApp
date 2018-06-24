using BCMLiteWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BCMLiteWebApp.Controllers.Api
{
    public class StepsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        public async Task<IHttpActionResult> GetPlanSteps(int organisationId)
        {
            throw new NotImplementedException();
        }
    }
}