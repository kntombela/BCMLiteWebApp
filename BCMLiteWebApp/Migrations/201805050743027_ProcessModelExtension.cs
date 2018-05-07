namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessModelExtension : DbMigration
    {
        public override void Up()
        {
            AddColumn("bia.Process", "CriticalTimeYear", c => c.String(maxLength: 255));
            AddColumn("bia.Process", "CriticalTimeMonth", c => c.String(maxLength: 255));
            AddColumn("bia.Process", "CriticalTimeDay", c => c.String(maxLength: 255));
            AddColumn("bia.Process", "CriticalTimeComment", c => c.String());
            AddColumn("bia.Process", "SOPComment", c => c.String());
            AddColumn("bia.Process", "SLAComment", c => c.String());
            AddColumn("bia.Process", "MTPD", c => c.String(maxLength: 50));
            AddColumn("bia.Process", "MTPDValue", c => c.Int());
            AddColumn("bia.Process", "WorkAreaComment", c => c.String());
            AlterColumn("bia.Process", "Description", c => c.String());
            AlterColumn("bia.Process", "RTO", c => c.String(maxLength: 50));
            AlterColumn("bia.Process", "StaffCompDesc", c => c.String());
            AlterColumn("bia.Process", "RevisedOpsLevelDesc", c => c.String());
            DropColumn("bia.Process", "CriticalTime");
        }
        
        public override void Down()
        {
            AddColumn("bia.Process", "CriticalTime", c => c.String(maxLength: 255));
            AlterColumn("bia.Process", "RevisedOpsLevelDesc", c => c.String(maxLength: 500));
            AlterColumn("bia.Process", "StaffCompDesc", c => c.String(maxLength: 500));
            AlterColumn("bia.Process", "RTO", c => c.String(maxLength: 255));
            AlterColumn("bia.Process", "Description", c => c.String(maxLength: 500));
            DropColumn("bia.Process", "WorkAreaComment");
            DropColumn("bia.Process", "MTPDValue");
            DropColumn("bia.Process", "MTPD");
            DropColumn("bia.Process", "SLAComment");
            DropColumn("bia.Process", "SOPComment");
            DropColumn("bia.Process", "CriticalTimeComment");
            DropColumn("bia.Process", "CriticalTimeDay");
            DropColumn("bia.Process", "CriticalTimeMonth");
            DropColumn("bia.Process", "CriticalTimeYear");
        }
    }
}
