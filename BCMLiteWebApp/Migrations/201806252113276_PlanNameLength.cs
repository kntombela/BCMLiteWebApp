namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("bcp.Plan", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("bcp.Plan", "Description", c => c.String(nullable: false));
            AlterColumn("bcp.Plan", "Type", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("bcp.Plan", "Type", c => c.String(maxLength: 11));
            AlterColumn("bcp.Plan", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("bcp.Plan", "Name", c => c.String(nullable: false, maxLength: 53));
        }
    }
}
