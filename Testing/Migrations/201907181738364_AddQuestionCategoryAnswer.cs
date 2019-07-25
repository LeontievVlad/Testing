namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionCategoryAnswer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Themes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Questions", "ThemeId", "dbo.Themes");
            DropIndex("dbo.Themes", new[] { "CategoryId" });
            DropIndex("dbo.Questions", new[] { "ThemeId" });
            AddColumn("dbo.Answers", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "ThemeName", c => c.String());
            AddColumn("dbo.Questions", "Description", c => c.String());
            AddColumn("dbo.Questions", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "CategoryId", c => c.Int());
            CreateIndex("dbo.Questions", "CategoryId");
            AddForeignKey("dbo.Questions", "CategoryId", "dbo.Categories", "Id");
            DropColumn("dbo.Answers", "ThemeId");
            DropColumn("dbo.Questions", "ThemeId");
            DropTable("dbo.Themes");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Questions", "ThemeId", c => c.Int());
            AddColumn("dbo.Answers", "ThemeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Questions", new[] { "CategoryId" });
            DropColumn("dbo.Questions", "CategoryId");
            DropColumn("dbo.Questions", "Time");
            DropColumn("dbo.Questions", "Description");
            DropColumn("dbo.Questions", "ThemeName");
            DropColumn("dbo.Answers", "CategoryId");
            CreateIndex("dbo.Questions", "ThemeId");
            CreateIndex("dbo.Themes", "CategoryId");
            AddForeignKey("dbo.Questions", "ThemeId", "dbo.Themes", "Id");
            AddForeignKey("dbo.Themes", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
