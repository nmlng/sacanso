namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_TaskaId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SubTaskas", name: "Taska", newName: "TaskaId");
            RenameIndex(table: "dbo.SubTaskas", name: "IX_Taska", newName: "IX_TaskaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SubTaskas", name: "IX_TaskaId", newName: "IX_Taska");
            RenameColumn(table: "dbo.SubTaskas", name: "TaskaId", newName: "Taska");
        }
    }
}
