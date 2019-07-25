namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullCategoryIdForAnswer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "CategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
