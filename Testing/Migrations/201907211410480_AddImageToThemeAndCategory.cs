namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToThemeAndCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImagePath", c => c.String(nullable: false));
            AddColumn("dbo.Themes", "ImagePath", c => c.String(nullable: false));
            AddColumn("dbo.Themes", "Author", c => c.String());
            AlterColumn("dbo.Categories", "NameCategory", c => c.String(nullable: false));
            AlterColumn("dbo.Themes", "ThemeName", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "NameQuestion", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "AnswerFirst", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "AnswerSecond", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "AnswerThird", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "AnswerForty", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "IsTrue", c => c.String(nullable: false));
            DropColumn("dbo.Categories", "Description");
            DropColumn("dbo.Themes", "Description");
            DropColumn("dbo.Themes", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Themes", "Time", c => c.Int(nullable: false));
            AddColumn("dbo.Themes", "Description", c => c.String());
            AddColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Questions", "IsTrue", c => c.String());
            AlterColumn("dbo.Questions", "AnswerForty", c => c.String());
            AlterColumn("dbo.Questions", "AnswerThird", c => c.String());
            AlterColumn("dbo.Questions", "AnswerSecond", c => c.String());
            AlterColumn("dbo.Questions", "AnswerFirst", c => c.String());
            AlterColumn("dbo.Questions", "NameQuestion", c => c.String());
            AlterColumn("dbo.Themes", "ThemeName", c => c.String());
            AlterColumn("dbo.Categories", "NameCategory", c => c.String());
            DropColumn("dbo.Themes", "Author");
            DropColumn("dbo.Themes", "ImagePath");
            DropColumn("dbo.Categories", "ImagePath");
        }
    }
}
