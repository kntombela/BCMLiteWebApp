using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCMLiteWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Organisations = new HashSet<Organisation>();
            DepartmentPlans = new HashSet<DepartmentPlan>();
        }

        //Additional properties for profile data
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Designation { get; set; }

        public bool MediaSpokesPerson { get; set; }

        public bool AuthorityToInvoke { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }

        public virtual ICollection<DepartmentPlan> DepartmentPlans { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Name", this.Name));
            userIdentity.AddClaim(new Claim("Surname", this.Surname));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BCMContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}