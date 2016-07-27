namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobName", c => c.String());
            DropColumn("dbo.Jobs", "ParameterName");
            DropColumn("dbo.Jobs", "ParameterValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "ParameterValue", c => c.String());
            AddColumn("dbo.Jobs", "ParameterName", c => c.String());
            DropColumn("dbo.Jobs", "JobName");
        }
    }
}
