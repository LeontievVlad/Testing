namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "ThemeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "ThemeId");
        }
    }
}
