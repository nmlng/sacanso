namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parameters");
            AddPrimaryKey("dbo.Parameters", new[] { "Name", "TaskaId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Parameters");
            AddPrimaryKey("dbo.Parameters", "Name");
        }
    }
}
