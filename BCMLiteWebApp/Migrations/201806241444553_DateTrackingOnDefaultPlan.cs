namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTrackingOnDefaultPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("bcp.DefaultPlans", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bcp.DefaultPlans", "DateModified", c => c.DateTime(nullable: false));

            Sql("CREATE TRIGGER tr_defaultPlans ON bcp.DefaultPlans AFTER UPDATE AS UPDATE bcp.DefaultPlans SET DateModified = GetDate() FROM bcp.DefaultPlans INNER JOIN inserted ON bcp.DefaultPlans.PlanID = inserted.PlanID");
        }
        
        public override void Down()
        {
            DropColumn("bcp.DefaultPlans", "DateModified");
            DropColumn("bcp.DefaultPlans", "DateCreated");
            Sql("DROP TRIGGER tr_defaultPlans");
        }
    }
}
