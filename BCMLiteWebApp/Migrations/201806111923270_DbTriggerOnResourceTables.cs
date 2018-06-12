namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbTriggerOnResourceTables : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER tr_application ON bia.Application AFTER UPDATE AS UPDATE bia.Application SET DateModified = GetDate() FROM bia.Application INNER JOIN inserted ON bia.Application.ApplicationID = inserted.ApplicationID");

            Sql("CREATE TRIGGER tr_thirdParty ON bia.ThirdParty AFTER UPDATE AS UPDATE bia.ThirdParty SET DateModified = GetDate() FROM bia.ThirdParty INNER JOIN inserted ON bia.ThirdParty.ThirdPartyID = inserted.ThirdPartyID");

            Sql("CREATE TRIGGER tr_document ON bia.Document AFTER UPDATE AS UPDATE bia.Document SET DateModified = GetDate() FROM bia.Document INNER JOIN inserted ON bia.Document.DocumentID = inserted.DocumentID");

            Sql("CREATE TRIGGER tr_skill ON bia.Skill AFTER UPDATE AS UPDATE bia.Skill SET DateModified = GetDate() FROM bia.Skill INNER JOIN inserted ON bia.Skill.SkillID = inserted.SkillID");

            Sql("CREATE TRIGGER tr_equipment ON bia.Equipment AFTER UPDATE AS UPDATE bia.Equipment SET DateModified = GetDate() FROM bia.Equipment INNER JOIN inserted ON bia.Equipment.EquipmentID = inserted.EquipmentID");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER tr_application");

            Sql("DROP TRIGGER tr_thirdParty");

            Sql("DROP TRIGGER tr_document");

            Sql("DROP TRIGGER tr_skill");

            Sql("DROP TRIGGER tr_equipment");
        }
    }
}
