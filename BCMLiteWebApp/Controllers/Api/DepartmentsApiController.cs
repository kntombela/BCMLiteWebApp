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
using System.Web.Http.Results;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/departments")]
    [Authorize]
    public class DepartmentsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET api/organisations/1/departments
        [Route("~/api/organisations/{organisationId:int}/departments")]
        [ResponseType(typeof(DepartmentViewModel))]
        public async Task<IHttpActionResult> GetDepartmentsByOrganisation(int organisationId)
        {
            var departments = await db.Departments.Where(d => d.OrganisationID == organisationId)
                                                  .Select(d => new DepartmentViewModel
                                                  {
                                                      DepartmentID = d.DepartmentID,
                                                      Name = d.Name,
                                                      Description = d.Description,
                                                      RevenueGenerating = d.RevenueGenerating,
                                                      Revenue = d.Revenue,
                                                      DateModified = d.DateModified,
                                                      OrganisationID = d.OrganisationID

                                                  }).ToListAsync();

            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // Get: api/departments/1
        [Route("{id:int}")]
        [ResponseType(typeof(DepartmentViewModel))]
        public async Task<IHttpActionResult> GetDepartmentById(int id)
        {
            var department = await db.Departments.Where(d => d.DepartmentID == id)
                                                  .Select(d => new DepartmentViewModel
                                                  {
                                                      DepartmentID = d.DepartmentID,
                                                      Name = d.Name,
                                                      Description = d.Description,
                                                      RevenueGenerating = d.RevenueGenerating,
                                                      Revenue = d.Revenue,
                                                      OrganisationID = d.OrganisationID

                                                  }).FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }


        // Post: api/departments
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> AddEditDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Determine if object has an id to choose between edit and update
            if (!DepartmentExists(department.DepartmentID))
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
            }
            else
            {
                db.Entry(department).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string status = DepartmentExists(department.DepartmentID) ? "updated" : "saved";
            string message = $"Department successfully { status }!";
            return Json(message);
        }

        // DELETE: api/departments/1
        [Route("{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteDepartment(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            await db.SaveChangesAsync();

            string message = "Department successfully deleted!";
            return Json(message);
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