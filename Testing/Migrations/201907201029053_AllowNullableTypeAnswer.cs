namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullableTypeAnswer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int());
            AlterColumn("dbo.Answers", "ThemeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "ThemeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int(nullable: false));
        }
    }
}
