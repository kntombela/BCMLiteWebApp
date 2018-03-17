namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDefaultCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("bcp.DefaultSteps", "CategoryID", "bcp.DefaultCategories");
            DropIndex("bcp.DefaultSteps", new[] { "CategoryID" });
            DropColumn("bcp.DefaultSteps", "CategoryID");
            DropTable("bcp.DefaultCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "bcp.DefaultCategories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 10),
                        Description = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("bcp.DefaultSteps", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("bcp.DefaultSteps", "CategoryID");
            AddForeignKey("bcp.DefaultSteps", "CategoryID", "bcp.DefaultCategories", "CategoryID", cascadeDelete: true);
        }
    }
}
