namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTemplateStepsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TemplateStep", "PlanID", "bcp.Plan");
            DropIndex("dbo.TemplateStep", new[] { "PlanID" });
            DropTable("dbo.TemplateStep");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TemplateStep",
                c => new
                    {
                        TemplateStepID = c.Int(nullable: false, identity: true),
                        PlanID = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        Number = c.Int(),
                        Title = c.String(maxLength: 100),
                        Summary = c.String(maxLength: 500),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.TemplateStepID);
            
            CreateIndex("dbo.TemplateStep", "PlanID");
            AddForeignKey("dbo.TemplateStep", "PlanID", "bcp.Plan", "PlanID", cascadeDelete: true);
        }
    }
}
