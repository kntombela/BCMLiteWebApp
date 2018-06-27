namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCategoryFromStep : DbMigration
    {
        public override void Up()
        {
            DropColumn("bcp.Step", "Category");
        }
        
        public override void Down()
        {
            AddColumn("bcp.Step", "Category", c => c.Int(nullable: false));
        }
    }
}
