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

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/equipments")]
    [Authorize]
    public class EquipmentApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/equipments
        [Route("~/api/processes/{processId:int}/equipments")]
        [ResponseType(typeof(DocumentViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var equipments = await GetEquipmentsByProcess(processId);

            if (equipments == null)
            {
                return NotFound();
            }

            return Ok(equipments);
        }

        // GET: api/equipments/1/details
        [ResponseType(typeof(EquipmentViewModel))]
        [Route("~/api/equipments/{id:int}/details")]
        public async Task<IHttpActionResult> GetEquipment(int id)
        {
            var equipments = await GetEquipmentById(id);

            if (equipments == null)
            {
                return NotFound();
            }

            return Ok(equipments);
        }

        // POST: api/equipments    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditEquipment(Equipment equipment)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!EquipmentExists(equipment.EquipmentID))
            {
                db.Equipments.Add(equipment);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(equipment).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                equipment.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EquipmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Equipment successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { equipment.EquipmentID }, Message = message });
        }

        // DELETE: api/equipments/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Equipment equipment = await db.Equipments.FindAsync(i);
                if (equipment == null)
                {
                    return NotFound();
                }

                db.Equipments.Remove(equipment);
            }
            await db.SaveChangesAsync();

            string message = "Equipment deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/equipments    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleEquipments(List<Equipment> equipmentList)
        {
            var equipmentIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in equipmentList)
            {
                db.Equipments.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in equipmentList)
            {
                equipmentIds.Add(data.EquipmentID);
            }

            status = "successful";

            string message = $"Equipment import { status }!";

            return Ok(new PostResponseViewModel { Ids = equipmentIds, Message = message });
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
        private bool EquipmentExists(int id)
        {
            return db.Equipments.Count(s => s.EquipmentID == id) > 0;
        }

        private async Task<List<EquipmentViewModel>> GetEquipmentsByProcess(int processId)
        {
            return await db.Equipments.Where(s => s.ProcessID == processId)
                                                  .Select(s => new EquipmentViewModel
                                                  {
                                                      EquipmentID = s.EquipmentID,
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).ToListAsync();
        }

        private async Task<EquipmentViewModel> GetEquipmentById(int id)
        {
            return await db.Equipments.Where(s => s.EquipmentID == id)
                                                  .Select(s => new EquipmentViewModel
                                                  {
                                                      EquipmentID = s.EquipmentID,
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).FirstOrDefaultAsync();
        }

        #endregion

    }
}