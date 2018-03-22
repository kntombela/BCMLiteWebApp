using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using System.Linq;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/departments")]
    [Authorize]
    public class DepartmentsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET api/organisations/1/departments
        [Route("~/api/organisations/{organisationId:int}/departments")]
        [ResponseType(typeof(DepartmentViewModels))]
        public async Task<IHttpActionResult> GetDepartmentsByOrganisation(int organisationId)
        {
            var departments = await db.Departments.Where(d => d.OrganisationID == organisationId)
                                                  .Select(d => new DepartmentViewModels
                                                  {
                                                      DepartmentID = d.DepartmentID,
                                                      Name = d.Name,
                                                      Description = d.Description,
                                                      RevenueGenerating = d.RevenueGenerating,
                                                      Revenue = d.Revenue
                
                                                  }).ToListAsync();

            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // GET: api/DepartmentsApi/5
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> GetDepartment(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/DepartmentsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentID)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DepartmentsApi
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> PostDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentID }, department);
        }

        // DELETE: api/DepartmentsApi/5
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> DeleteDepartment(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            await db.SaveChangesAsync();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentID == id) > 0;
        }
    }
}