using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCMLiteWebApp.Controllers
{

    [Authorize]
    public class DepartmentsController : Controller
    {
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private BCMContext db = new BCMContext();

        public DepartmentsController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET: Departments
        public async Task<ActionResult> Index()
        {

            //if user is admin present a drop down list to select available organisations
            ViewBag.IsAdmin = false;
            var model = new OrganisationDropDownViewModel();

            if (IsAdminUser())
            {
                ViewBag.IsAdmin = true;
            }

            var departments = db.Departments.Include(d => d.Organisation);
            return View(await departments.ToListAsync());

        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.OrganisationID = new SelectList(db.Organisations, "OrganisationID", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Description,RevenueGenerating,Revenue,OrganisationID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrganisationID = new SelectList(db.Organisations, "OrganisationID", "Name", department.OrganisationID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "OrganisationID", "Name", department.OrganisationID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentID,Name,Description,RevenueGenerating,Revenue,OrganisationID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "OrganisationID", "Name", department.OrganisationID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var s = UserManager.GetRoles(User.Identity.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}
