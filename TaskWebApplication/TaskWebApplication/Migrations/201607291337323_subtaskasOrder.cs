namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subtaskasOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTaskas", "order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubTaskas", "order");
        }
    }
}
