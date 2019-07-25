using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Testing.Models;


namespace Testing.Controllers
{
    public class QuestionsController : Controller
    {
        private TestContext db = new TestContext();
        public const int TimeForTest = 20*3600;//20*3600=20min
        // GET: Questions
        [Authorize]
        public ActionResult Index()
        {
            
            var questions = db.Questions.Include(q => q.Theme);
            return View(questions.ToList());
        }

        //Get: ViewPage
        [HttpGet]
        public ActionResult ViewPage()
        {
            //ViewBag.AllCategories = new SelectList(db.Categories.OrderBy(c => c.NameCategory), "Id", "NameCategory");
            var category = db.Categories.OrderBy(x=>x.NameCategory).ToList();
            
            return View(category);
        }

        [HttpPost]
        public ActionResult ViewTheme(int? category)
        {
  
            var themes = db.Themes.Where(x => x.CategoryId == category).OrderBy(x => x.ThemeName).ToList();
            //ViewBag.AllThemes = new SelectList(themes, "Id", "ThemeName");
            if (themes.Count() == 0)
                ViewBag.NoItems = "В цій категорії немає тестів, ви можете почекати поки ми додамо їх або додати самі!";
            else
                ViewBag.NoItems = "";
            return View(themes);
        }

        [HttpPost]
        public ActionResult TakeYourName(int? theme)
        {
            ViewBag.Theme = theme;
            
            return View();
        }

        [HttpGet]
        public ActionResult ViewQuestion(int? themeQuestion, string personName)
        {
            
            var questions = db.Questions.Include(q => q.Theme).Where(x => x.ThemeId == themeQuestion)
                .OrderBy(x => x.Id);
            int questionCount = 1;
            ViewBag.Time = TimeForTest;//20*3600;
            ViewBag.Count = questionCount;
            ViewBag.Name = (personName+DateTime.Now).ToString();
            return View(questions.ToList());
        }

        [HttpPost]
        public ActionResult ViewQuestion(string YourName, int? questionId,
            int? themeId,int? categoryId, int questionCount, string answerY,
            string isTrue, int? allTime)
        {
            


            Answer answer = new Answer();
            answer.PersonName = YourName;
            answer.AnswerName = answerY;
            answer.QuestionId = questionId;
            answer.ThemeId = themeId;
            answer.CategoryId = categoryId;
            db.Answers.Add(answer);
            db.SaveChanges();
            

            var questions = db.Questions.Include(q => q.Theme).Where(x => x.ThemeId == themeId)
                .OrderBy(x => x.Id)
                .Skip(questionCount);

            ViewBag.Name = YourName;
            ViewBag.Time = allTime;
            questionCount++;
            ViewBag.Count = questionCount;
            
            var sumItem = questions.Count();


            //перевірка на пробну здачу
            var answerCheck = db.Answers.Where(x => x.PersonName == YourName && x.CategoryId == categoryId &&
            x.ThemeId == themeId).Count();
            var questionCheck = db.Questions.Where(x => x.ThemeId == themeId).Count();
           

            if (answerCheck > questionCheck)
            {
                return RedirectToAction("Result",
                    new
                    {
                        result = "FailTest"
                                    
                });

            }

            if (sumItem == 0)
            {
                return RedirectToAction("Result",
                new {
                    result = answer.PersonName,
                    categoryId = answer.CategoryId,
                    questionId = answer.QuestionId,
                    themeId = answer.ThemeId
                });
            }
            return View(questions.ToList());

        }

        public ActionResult Result(string result, int? themeId, int? categoryId)
        {
            ViewBag.Name = result;
            if (result == "FailTest")
            {
                ViewBag.YourPoints = "Тест не пройдено!!!(деталі у правилах)";
                return View();
            }

            var answer = db.Answers.Where(x => x.PersonName == result && x.CategoryId == categoryId &&
            x.ThemeId == themeId).OrderBy(x=>x.QuestionId).ToList();
            //var questionsCheck = db.Questions.Where(x => x.ThemeId == themeId).OrderBy(x => x.Id).ToList();
            var questions = db.Questions.Where(x => x.ThemeId == themeId).OrderBy(x => x.Id).ToList();
            

            int points = 0;
            int allAnswers = 0;
            List<string> ans = new List<string> { };
            List<string> quest = new List<string> { };
            foreach (var answers in answer)
            {
                ans.Add(answers.AnswerName);
            }
            foreach (var qst in questions)
            {
                quest.Add(qst.IsTrue);
            }

            for (var i = 0; i < ans.Count(); i++)
            {
                allAnswers++;
                if (ans[i] == quest[i])
                    points++;
            }

            
            ViewBag.YourAnswer = ans;
            points = (points * 100) / allAnswers;
            ViewBag.YourPoints = points + "%";
            return View(questions);
        }

        [HttpGet]
        public ActionResult AddNewTheme()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameCategory");
            return View();
        }


        [HttpPost]
        public ActionResult AddNewTheme(Theme theme)
        {
            string filename = Path.GetFileNameWithoutExtension(theme.ImageFile.FileName);
            string extension = Path.GetExtension(theme.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            theme.ImagePath = "/Images/" + filename;
            filename = Path.Combine(Server.MapPath("/Images/"), filename);
            theme.ImageFile.SaveAs(filename);

            db.Themes.Add(theme);
            db.SaveChanges();

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameCategory", theme.CategoryId);
            return RedirectToAction("Create", new { themeId = theme.Id});
            
            
           
        }

        // GET: Questions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        
        public ActionResult Create(int? themeId)
        {
            ViewBag.ThemeId = themeId;
            //ViewBag.ThemeId = new SelectList(db.Themes, "Id", "ThemeName");
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameCategory");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameQuestion,AnswerFirst,AnswerSecond,AnswerThird,AnswerForty,IsTrue,ThemeId,ImagePath,ImageFile")] Question question)
        {

            string filename = Path.GetFileNameWithoutExtension(question.ImageFile.FileName);
            string extension = Path.GetExtension(question.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            question.ImagePath = "/Images/" + filename;
            filename = Path.Combine(Server.MapPath("/Images/"), filename);
            question.ImageFile.SaveAs(filename);
           
            db.Questions.Add(question);
            db.SaveChanges();
            return RedirectToAction("Create", new { themeId = question.ThemeId});
            
            //ViewBag.ThemeId = themeId;
            //ViewBag.ThemeId = new SelectList(db.Themes, "Id", "ThemeName", question.ThemeId);
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameCategory", question.Theme.CategoryId);
            //return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThemeId = new SelectList(db.Themes, "Id", "ThemeName", question.ThemeId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,NameQuestion,AnswerFirst,AnswerSecond,AnswerThird,AnswerForty,IsTrue,ThemeId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThemeId = new SelectList(db.Themes, "Id", "ThemeName", question.ThemeId);
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
