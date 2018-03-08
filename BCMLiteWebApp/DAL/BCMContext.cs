using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BCMLiteWebApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCMLiteWebApp.DAL
{

    public partial class BCMContext : IdentityDbContext<ApplicationUser>
    {
        public BCMContext()
            : base("name=BCMContext")
        {

        }

        public virtual DbSet<DefaultCategory> DefaultCategories { get; set; }
        public virtual DbSet<DefaultPlan> DefaultPlans { get; set; }
        public virtual DbSet<DefaultStep> DefaultSteps { get; set; }
        public virtual DbSet<DepartmentPlan> DepartmentPlans { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ThirdParty> ThirdParties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Organisations)
                .WithMany(o => o.Users)
                .Map(uc => uc.ToTable("OrganisationApplicationUsers"));

            base.OnModelCreating(modelBuilder);

        }
    }
}
