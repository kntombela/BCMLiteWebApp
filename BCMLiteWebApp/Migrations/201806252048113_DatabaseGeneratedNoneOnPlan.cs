namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseGeneratedNoneOnPlan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("bcp.DepartmentPlan", "PlanID", "bcp.Plan");
            DropPrimaryKey("bcp.Plan");
            AddColumn("bcp.Plan", "Abbreviation", c => c.String(maxLength: 6));
            AlterColumn("bcp.Plan", "PlanID", c => c.Int(nullable: false));
            AddPrimaryKey("bcp.Plan", "PlanID");
            AddForeignKey("bcp.DepartmentPlan", "PlanID", "bcp.Plan", "PlanID", cascadeDelete: true);
            DropColumn("bcp.Plan", "PlanAbbreviation");
        }
        
        public override void Down()
        {
            AddColumn("bcp.Plan", "PlanAbbreviation", c => c.String(maxLength: 6));
            DropForeignKey("bcp.DepartmentPlan", "PlanID", "bcp.Plan");
            DropPrimaryKey("bcp.Plan");
            AlterColumn("bcp.Plan", "PlanID", c => c.Int(nullable: false, identity: true));
            DropColumn("bcp.Plan", "Abbreviation");
            AddPrimaryKey("bcp.Plan", "PlanID");
            AddForeignKey("bcp.DepartmentPlan", "PlanID", "bcp.Plan", "PlanID", cascadeDelete: true);
        }
    }
}
