namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskaDescriptionRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Taskas", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Taskas", "Description", c => c.String());
        }
    }
}
