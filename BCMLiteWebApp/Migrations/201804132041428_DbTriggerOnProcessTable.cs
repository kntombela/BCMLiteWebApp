namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbTriggerOnProcessTable : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER tr_process ON bia.Process AFTER UPDATE AS UPDATE bia.Process SET DateModified = GetDate() FROM bia.Process INNER JOIN inserted ON bia.Process.ProcessID = inserted.ProcessID");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER tr_process");
        }
    }
}
