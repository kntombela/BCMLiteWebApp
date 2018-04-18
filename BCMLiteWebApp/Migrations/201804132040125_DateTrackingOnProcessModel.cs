namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTrackingOnProcessModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("bia.Process", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.Process", "DateModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("bia.Process", "DateModified");
            DropColumn("bia.Process", "DateCreated");
        }
    }
}
