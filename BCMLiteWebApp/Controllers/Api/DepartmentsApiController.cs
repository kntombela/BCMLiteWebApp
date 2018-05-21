using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/departments")]
    [Authorize]
    public class DepartmentsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/organisations/1/departments
        [Route("~/api/organisations/{organisationId:int}/departments")]
        [ResponseType(typeof(DepartmentViewModel))]
        public async Task<IHttpActionResult> GetDepartments(int organisationId)
        {
            var departments = await GetDepartmentsByOrganisation(organisationId);

            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // GET: api/departments/1
        [Route("{id:int}")]
        [ResponseType(typeof(DepartmentViewModel))]
        public async Task<IHttpActionResult> GetDepartment(int id)
        {
            var department = await GetDepartmentById(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    
        // POST: api/departments    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditDepartment(Department department)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!DepartmentExists(department.DepartmentID))
            {             
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(department).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                department.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
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
          
            string message = $"Department successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() {department.DepartmentID}, Message = message });
        }

        // POST: api/processes    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleDepartments(List<Department> departmentList)
        {
            var departmentIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in departmentList)
            {
                db.Departments.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in departmentList)
            {
                departmentIds.Add(data.DepartmentID);
            }
            status = "successful";

            string message = $"Department import { status }!";

            return Ok(new PostResponseViewModel { Ids = departmentIds, Message = message });
        }

        // DELETE: api/departments/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach(int i in ids)
            {
                Department department = await db.Departments.FindAsync(i);
                if (department == null)
                {
                    return NotFound();
                }

                db.Departments.Remove(department);
            }
            await db.SaveChangesAsync();

            string message = "Departments deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });    
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

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentID == id) > 0;
        }

        private async Task<List<DepartmentViewModel>> GetDepartmentsByOrganisation(int organisationId)
        {
            return await db.Departments.Where(d => d.OrganisationID == organisationId)
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
        }

        private async Task<DepartmentViewModel> GetDepartmentById(int id)
        {
            return await db.Departments.Where(d => d.DepartmentID == id)
                                                  .Select(d => new DepartmentViewModel
                                                  {
                                                      DepartmentID = d.DepartmentID,
                                                      Name = d.Name,
                                                      Description = d.Description,
                                                      RevenueGenerating = d.RevenueGenerating,
                                                      Revenue = d.Revenue,
                                                      OrganisationID = d.OrganisationID
                                                  }).FirstOrDefaultAsync();
        }

        #endregion
    }
}