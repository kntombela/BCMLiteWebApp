namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Department", "DateModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Department", "DateModified");
            DropColumn("dbo.Department", "DateCreated");
        }
    }
}
