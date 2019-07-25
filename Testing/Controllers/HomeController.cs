using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Models;

namespace Testing.Controllers
{
    public class HomeController : Controller
    {
        private TestContext db = new TestContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameCategory,ImagePath,ImageFile")]Category category)
        {
            string filename = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
            string extension = Path.GetExtension(category.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            category.ImagePath = "/Images/" + filename;
            filename = Path.Combine(Server.MapPath("/Images/"), filename);
            category.ImageFile.SaveAs(filename);
           
            db.Categories.Add(category);
            db.SaveChanges();
            return Redirect("/Questions/ViewPage");
            
            
        }
    }
}