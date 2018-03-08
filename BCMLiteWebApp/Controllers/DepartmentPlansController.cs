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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCMLiteWebApp.Controllers
{
    public class DepartmentPlansController : Controller
    {
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private BCMContext db = new BCMContext();

        public DepartmentPlansController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET: DepartmentPlans
        public ActionResult Index()
        {
            //if user is admin present a drop down list to select available organisations and associated plans
            ViewBag.IsAdmin = false;
            if (IsAdminUser())
            {
                ViewBag.IsAdmin = true;
                ViewBag.OrganisationID = new SelectList(db.Organisations, "OrganisationID", "Name");
            }

            return View();
        }

        // GET: DepartmentPlans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentPlan departmentPlan = await db.DepartmentPlans.FindAsync(id);
            if (departmentPlan == null)
            {
                return HttpNotFound();
            }
            return View(departmentPlan);
        }

        // GET: DepartmentPlans/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanAbbreviation");
            return View();
        }

        // POST: DepartmentPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentPlanID,DepartmentID,PlanID,DepartmentPlanInvoked")] DepartmentPlan departmentPlan)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentPlans.Add(departmentPlan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", departmentPlan.DepartmentID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanAbbreviation", departmentPlan.PlanID);
            return View(departmentPlan);
        }

        // GET: DepartmentPlans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentPlan departmentPlan = await db.DepartmentPlans.FindAsync(id);
            if (departmentPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", departmentPlan.DepartmentID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanAbbreviation", departmentPlan.PlanID);
            return View(departmentPlan);
        }

        // POST: DepartmentPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentPlanID,DepartmentID,PlanID,DepartmentPlanInvoked")] DepartmentPlan departmentPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentPlan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", departmentPlan.DepartmentID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanAbbreviation", departmentPlan.PlanID);
            return View(departmentPlan);
        }

        // GET: DepartmentPlans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentPlan departmentPlan = await db.DepartmentPlans.FindAsync(id);
            if (departmentPlan == null)
            {
                return HttpNotFound();
            }
            return View(departmentPlan);
        }

        // POST: DepartmentPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DepartmentPlan departmentPlan = await db.DepartmentPlans.FindAsync(id);
            db.DepartmentPlans.Remove(departmentPlan);
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
