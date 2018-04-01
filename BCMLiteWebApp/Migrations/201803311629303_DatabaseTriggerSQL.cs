namespace BCMLiteWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseTriggerSQL : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER tr_department ON dbo.Department AFTER UPDATE AS UPDATE dbo.Department SET DateModified = GetDate() FROM dbo.Department INNER JOIN inserted ON dbo.Department.DepartmentID = inserted.DepartmentID");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER tr_department");
        }
    }
}
