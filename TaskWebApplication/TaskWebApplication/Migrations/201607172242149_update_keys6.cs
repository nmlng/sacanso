namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys6 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parameters");
            AddPrimaryKey("dbo.Parameters", new[] { "Name", "TaskaId" });
            DropColumn("dbo.Parameters", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parameters", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Parameters");
            AddPrimaryKey("dbo.Parameters", new[] { "Id", "Name", "TaskaId" });
        }
    }
}
