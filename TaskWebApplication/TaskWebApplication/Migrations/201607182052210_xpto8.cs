namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto8 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SubTaskas", name: "TaskaId", newName: "Taska");
            RenameIndex(table: "dbo.SubTaskas", name: "IX_TaskaId", newName: "IX_Taska");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SubTaskas", name: "IX_Taska", newName: "IX_TaskaId");
            RenameColumn(table: "dbo.SubTaskas", name: "Taska", newName: "TaskaId");
        }
    }
}
