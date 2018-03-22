namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevenueFieldAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "Revenue", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Department", "Revenue");
        }
    }
}
