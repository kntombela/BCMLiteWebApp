namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevenueFieldTypeUpdate : DbMigration
    {
        public override void Up()
        {
       
        }
        
        public override void Down()
        {
            AddColumn("dbo.Department", "Revenue", c => c.String(maxLength: 8));
        }
    }
}
