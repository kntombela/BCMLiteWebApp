namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationIdFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Department", "OrganisationID", "dbo.Organisation");
            DropIndex("dbo.Department", new[] { "OrganisationID" });
            AlterColumn("dbo.Department", "OrganisationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Department", "OrganisationID");
            AddForeignKey("dbo.Department", "OrganisationID", "dbo.Organisation", "OrganisationID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Department", "OrganisationID", "dbo.Organisation");
            DropIndex("dbo.Department", new[] { "OrganisationID" });
            AlterColumn("dbo.Department", "OrganisationID", c => c.Int());
            CreateIndex("dbo.Department", "OrganisationID");
            AddForeignKey("dbo.Department", "OrganisationID", "dbo.Organisation", "OrganisationID");
        }
    }
}
