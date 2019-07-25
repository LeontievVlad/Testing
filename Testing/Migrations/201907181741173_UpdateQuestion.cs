namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuestion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "IsTrue", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "IsTrue", c => c.Boolean(nullable: false));
        }
    }
}
