using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/equipment")]
    [Authorize]
    public class EquipmentApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/equipment
        [Route("~/api/processes/{processId:int}/equipment")]
        [ResponseType(typeof(DocumentViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var equipment = await GetEquipmentByProcess(processId);

            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        private async Task<List<EquipmentViewModel>> GetEquipmentByProcess(int processId)
        {
            return await db.Equipments.Where(e => e.ProcessID == processId)
                                                  .Select(e => new EquipmentViewModel
                                                  {
                                                      EquipmentID = e.EquipmentID,
                                                      Description = e.Description,
                                                      RTO = e.RTO,
                                                      ProcessID = e.ProcessID
                                                  }).ToListAsync();
        }

    }
}