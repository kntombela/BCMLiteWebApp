namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTrackingOnDefaultStepModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("bcp.DefaultSteps", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bcp.DefaultSteps", "DateModified", c => c.DateTime(nullable: false));
            Sql("CREATE TRIGGER tr_defaultStep ON bcp.DefaultSteps AFTER UPDATE AS UPDATE bcp.DefaultSteps SET DateModified = GetDate() FROM bcp.DefaultSteps INNER JOIN inserted ON bcp.DefaultSteps.StepID = inserted.StepID");
        }
        
        public override void Down()
        {
            DropColumn("bcp.DefaultSteps", "DateModified");
            DropColumn("bcp.DefaultSteps", "DateCreated");
            Sql("DROP TRIGGER tr_defaultStep");
        }
    }
}
