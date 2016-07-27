namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parameters");
            AlterColumn("dbo.Parameters", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Parameters", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Parameters", new[] { "Id", "Name" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Parameters");
            AlterColumn("dbo.Parameters", "Name", c => c.String());
            AlterColumn("dbo.Parameters", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Parameters", "Id");
        }
    }
}
