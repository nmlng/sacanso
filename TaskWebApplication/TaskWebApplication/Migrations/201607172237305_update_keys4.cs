namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_keys4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Parameters", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parameters", "Id", c => c.Int(nullable: false));
        }
    }
}
