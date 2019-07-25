using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class TestContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public System.Data.Entity.DbSet<Testing.Models.Theme> Themes { get; set; }
    }

    //public class TestDbInitializer : DropCreateDatabaseAlways<TestContext>
    //{
    //    protected override void Seed(TestContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}