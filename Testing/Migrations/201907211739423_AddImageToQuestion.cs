namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ImagePath");
        }
    }
}
