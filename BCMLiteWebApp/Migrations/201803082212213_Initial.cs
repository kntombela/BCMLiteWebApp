namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
                        
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("bcp.DefaultSteps", "PlanID", "bcp.DefaultPlans");
            DropForeignKey("bcp.DefaultSteps", "CategoryID", "bcp.DefaultCategories");
            DropForeignKey("bia.ThirdParty", "ProcessID", "bia.Process");
            DropForeignKey("bia.Skill", "ProcessID", "bia.Process");
            DropForeignKey("bia.Equipment", "ProcessID", "bia.Process");
            DropForeignKey("bia.Document", "ProcessID", "bia.Process");
            DropForeignKey("bia.Process", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrganisationApplicationUsers", "Organisation_OrganisationID", "dbo.Organisation");
            DropForeignKey("dbo.OrganisationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Incident", "OrganisationID", "dbo.Organisation");
            DropForeignKey("dbo.Department", "OrganisationID", "dbo.Organisation");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserDepartmentPlan", "DepartmentPlan_DepartmentPlanID", "bcp.DepartmentPlan");
            DropForeignKey("dbo.ApplicationUserDepartmentPlan", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("bcp.Step", "DepartmentPlanID", "bcp.DepartmentPlan");
            DropForeignKey("bcp.DepartmentPlan", "PlanID", "bcp.Plan");
            DropForeignKey("bcp.DepartmentPlan", "DepartmentID", "dbo.Department");
            DropForeignKey("bia.Application", "ProcessID", "bia.Process");
            DropIndex("dbo.OrganisationApplicationUsers", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.OrganisationApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserDepartmentPlan", new[] { "DepartmentPlan_DepartmentPlanID" });
            DropIndex("dbo.ApplicationUserDepartmentPlan", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("bcp.DefaultSteps", new[] { "CategoryID" });
            DropIndex("bcp.DefaultSteps", new[] { "PlanID" });
            DropIndex("bia.ThirdParty", new[] { "ProcessID" });
            DropIndex("bia.Skill", new[] { "ProcessID" });
            DropIndex("bia.Equipment", new[] { "ProcessID" });
            DropIndex("bia.Document", new[] { "ProcessID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Incident", new[] { "OrganisationID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("bcp.Step", new[] { "DepartmentPlanID" });
            DropIndex("bcp.DepartmentPlan", new[] { "PlanID" });
            DropIndex("bcp.DepartmentPlan", new[] { "DepartmentID" });
            DropIndex("dbo.Department", new[] { "OrganisationID" });
            DropIndex("bia.Process", new[] { "DepartmentID" });
            DropIndex("bia.Application", new[] { "ProcessID" });
            DropTable("dbo.OrganisationApplicationUsers");
            DropTable("dbo.ApplicationUserDepartmentPlan");
            DropTable("dbo.AspNetRoles");
            DropTable("bcp.DefaultPlans");
            DropTable("bcp.DefaultSteps");
            DropTable("bcp.DefaultCategories");
            DropTable("bia.ThirdParty");
            DropTable("bia.Skill");
            DropTable("bia.Equipment");
            DropTable("bia.Document");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Incident");
            DropTable("dbo.Organisation");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("bcp.Step");
            DropTable("bcp.Plan");
            DropTable("bcp.DepartmentPlan");
            DropTable("dbo.Department");
            DropTable("bia.Process");
            DropTable("bia.Application");
        }
    }
}
