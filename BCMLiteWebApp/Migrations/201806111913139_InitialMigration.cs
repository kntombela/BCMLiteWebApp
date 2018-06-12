namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("bia.Application", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.Application", "DateModified", c => c.DateTime(nullable: false));

            AddColumn("bia.Skill", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.Skill", "DateModified", c => c.DateTime(nullable: false));

            AddColumn("bia.ThirdParty", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.ThirdParty", "DateModified", c => c.DateTime(nullable: false));

            AddColumn("bia.Document", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.Document", "DateModified", c => c.DateTime(nullable: false));

            AddColumn("bia.Equipment", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("bia.Equipment", "DateModified", c => c.DateTime(nullable: false));

        }
        
        public override void Down()
        {
            DropColumn("bia.Application", "DateModified");
            DropColumn("bia.Application", "DateCreated");

            DropColumn("bia.Skill", "DateModified");
            DropColumn("bia.Skill", "DateCreated");

            DropColumn("bia.ThirdParty", "DateModified");
            DropColumn("bia.ThirdParty", "DateCreated");

            DropColumn("bia.Document", "DateModified");
            DropColumn("bia.Document", "DateCreated");

            DropColumn("bia.Equipment", "DateModified");
            DropColumn("bia.Equipment", "DateCreated");
        }
    }
}
