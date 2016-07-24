namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parameters");
            AddColumn("dbo.Parameters", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Parameters", new[] { "Id", "Name", "TaskaId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Parameters");
            DropColumn("dbo.Parameters", "Id");
            AddPrimaryKey("dbo.Parameters", new[] { "Name", "TaskaId" });
        }
    }
}
