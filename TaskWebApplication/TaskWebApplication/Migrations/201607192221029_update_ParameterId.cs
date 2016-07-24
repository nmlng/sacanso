namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_ParameterId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobParameters", name: "Parameter", newName: "ParameterId");
            RenameIndex(table: "dbo.JobParameters", name: "IX_Parameter", newName: "IX_ParameterId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobParameters", name: "IX_ParameterId", newName: "IX_Parameter");
            RenameColumn(table: "dbo.JobParameters", name: "ParameterId", newName: "Parameter");
        }
    }
}
