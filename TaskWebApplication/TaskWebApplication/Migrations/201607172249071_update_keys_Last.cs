namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys_Last : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parameters");
            AddColumn("dbo.Parameters", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Parameters", "Name", c => c.String());
            AddPrimaryKey("dbo.Parameters", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Parameters");
            AlterColumn("dbo.Parameters", "Name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Parameters", "Id");
            AddPrimaryKey("dbo.Parameters", new[] { "Name", "TaskaId" });
        }
    }
}
