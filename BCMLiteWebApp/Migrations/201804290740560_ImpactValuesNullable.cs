namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImpactValuesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("bia.Process", "OperationalImpactValue", c => c.Int());
            AlterColumn("bia.Process", "FinancialImpactValue", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("bia.Process", "FinancialImpactValue", c => c.Int(nullable: false));
            AlterColumn("bia.Process", "OperationalImpactValue", c => c.Int(nullable: false));
        }
    }
}
