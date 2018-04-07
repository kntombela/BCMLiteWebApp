namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationRules : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Department", "Name", c => c.String(nullable: false, maxLength: 28));
            AlterColumn("dbo.Department", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Department", "Description", c => c.String(maxLength: 105));
            AlterColumn("dbo.Department", "Name", c => c.String(maxLength: 28));
        }
    }
}
