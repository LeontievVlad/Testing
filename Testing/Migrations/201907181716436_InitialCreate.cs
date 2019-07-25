namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonName = c.String(),
                        AnswerName = c.String(),
                        ThemeId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Time = c.DateTime(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameQuestion = c.String(),
                        AnswerFirst = c.String(),
                        AnswerSecond = c.String(),
                        AnswerThird = c.String(),
                        AnswerForty = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        ThemeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.ThemeId)
                .Index(t => t.ThemeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Questions", new[] { "ThemeId" });
            DropIndex("dbo.Themes", new[] { "CategoryId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Themes");
            DropTable("dbo.Categories");
            DropTable("dbo.Answers");
        }
    }
}
