namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parameters", "ParameterName", c => c.String(nullable: false));
            AlterColumn("dbo.SubTaskas", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.JobResults", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobResults", "Description", c => c.String());
            AlterColumn("dbo.SubTaskas", "Description", c => c.String());
            AlterColumn("dbo.Parameters", "ParameterName", c => c.String());
        }
    }
}
