namespace Testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTheme : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Questions", new[] { "CategoryId" });
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(),
                        Description = c.String(),
                        Time = c.Int(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Questions", "ThemeId", c => c.Int());
            CreateIndex("dbo.Questions", "ThemeId");
            AddForeignKey("dbo.Questions", "ThemeId", "dbo.Themes", "Id");
            DropColumn("dbo.Questions", "ThemeName");
            DropColumn("dbo.Questions", "Description");
            DropColumn("dbo.Questions", "Time");
            DropColumn("dbo.Questions", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "CategoryId", c => c.Int());
            AddColumn("dbo.Questions", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "Description", c => c.String());
            AddColumn("dbo.Questions", "ThemeName", c => c.String());
            DropForeignKey("dbo.Questions", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Questions", new[] { "ThemeId" });
            DropIndex("dbo.Themes", new[] { "CategoryId" });
            DropColumn("dbo.Questions", "ThemeId");
            DropTable("dbo.Themes");
            CreateIndex("dbo.Questions", "CategoryId");
            AddForeignKey("dbo.Questions", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
