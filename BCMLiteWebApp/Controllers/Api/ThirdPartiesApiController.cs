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
    [RoutePrefix("api/thirdparties")]
    [Authorize]
    public class ThirdPartiesApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/thirdparties
        [Route("~/api/processes/{processId:int}/thirdparties")]
        [ResponseType(typeof(ThirdPartyViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var thirdparties = await GetThirdPartiesByProcess(processId);

            if (thirdparties == null)
            {
                return NotFound();
            }

            return Ok(thirdparties);
        }

        // GET: api/thirdparties/1/details
        [ResponseType(typeof(ThirdPartyViewModel))]
        [Route("~/api/thirdparties/{id:int}/details")]
        public async Task<IHttpActionResult> GetThirdParty(int id)
        {
            var thirdparties = await GetThirdPartyById(id);

            if (thirdparties == null)
            {
                return NotFound();
            }

            return Ok(thirdparties);
        }

        // POST: api/thirdparties    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditThirdParty(ThirdParty thirdParty)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!ThirdPartyExists(thirdParty.ThirdPartyID))
            {
                db.ThirdParties.Add(thirdParty);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(thirdParty).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                thirdParty.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThirdPartyExists(thirdParty.ThirdPartyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"ThirdParty successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { thirdParty.ThirdPartyID }, Message = message });
        }

        // DELETE: api/thirdparties/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                ThirdParty thirdParty = await db.ThirdParties.FindAsync(i);
                if (thirdParty == null)
                {
                    return NotFound();
                }

                db.ThirdParties.Remove(thirdParty);
            }
            await db.SaveChangesAsync();

            string message = "Third Parties deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/thirdparties    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleThirdParties(List<ThirdParty> thirdPartyList)
        {
            var thirdPartyIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in thirdPartyList)
            {
                db.ThirdParties.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in thirdPartyList)
            {
                thirdPartyIds.Add(data.ThirdPartyID);
            }

            status = "successful";

            string message = $"ThirdParty import { status }!";

            return Ok(new PostResponseViewModel { Ids = thirdPartyIds, Message = message });
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
        private bool ThirdPartyExists(int id)
        {
            return db.ThirdParties.Count(t => t.ThirdPartyID == id) > 0;
        }

        private async Task<List<ThirdPartyViewModel>> GetThirdPartiesByProcess(int processId)
        {
            return await db.ThirdParties.Where(t => t.ProcessID == processId)
                                                  .Select(t => new ThirdPartyViewModel
                                                  {
                                                      ThirdPartyID = t.ThirdPartyID,
                                                      Name = t.Name,
                                                      Description = t.Description,
                                                      RTO = t.RTO,
                                                      ProcessID = t.ProcessID,
                                                      DateModified = t.DateModified
                                                  }).ToListAsync();
        }

        private async Task<ThirdPartyViewModel> GetThirdPartyById(int id)
        {
            return await db.ThirdParties.Where(t => t.ThirdPartyID == id)
                                                  .Select(t => new ThirdPartyViewModel
                                                  {
                                                      ThirdPartyID = t.ThirdPartyID,
                                                      Name = t.Name,
                                                      Description = t.Description,
                                                      RTO = t.RTO,
                                                      ProcessID = t.ProcessID,
                                                      DateModified = t.DateModified
                                                  }).FirstOrDefaultAsync();
        }

        #endregion

    }
}