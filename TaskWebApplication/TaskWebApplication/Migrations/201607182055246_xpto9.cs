namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parameters", "ParameterName", c => c.String());
            AddColumn("dbo.Parameters", "ParameterValue", c => c.String());
            DropColumn("dbo.Parameters", "Name");
            DropColumn("dbo.Parameters", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parameters", "Value", c => c.String());
            AddColumn("dbo.Parameters", "Name", c => c.String());
            DropColumn("dbo.Parameters", "ParameterValue");
            DropColumn("dbo.Parameters", "ParameterName");
        }
    }
}
