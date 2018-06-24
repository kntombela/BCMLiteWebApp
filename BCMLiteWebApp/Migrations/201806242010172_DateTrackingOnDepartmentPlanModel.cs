namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTrackingOnDepartmentPlanModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("bcp.DepartmentPlan", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bcp.DepartmentPlan", "DateModified", c => c.DateTime(nullable: false));
            Sql("CREATE TRIGGER tr_departmentPlan ON bcp.DepartmentPlan AFTER UPDATE AS UPDATE bcp.DepartmentPlan SET DateModified = GetDate() FROM bcp.DepartmentPlan INNER JOIN inserted ON bcp.DepartmentPlan.DepartmentPlanID = inserted.DepartmentPlanID");
        }
        
        public override void Down()
        {
            DropColumn("bcp.DepartmentPlan", "DateModified");
            DropColumn("bcp.DepartmentPlan", "DateCreated");
            Sql("DROP TRIGGER tr_departmentPlan");
        }
    }
}
