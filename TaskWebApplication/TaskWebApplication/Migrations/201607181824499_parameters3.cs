namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameters3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters");
            DropIndex("dbo.Jobs", new[] { "Parameter_Id" });
            DropColumn("dbo.Jobs", "Parameter_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Parameter_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Parameter_Id");
            AddForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters", "Id");
        }
    }
}
