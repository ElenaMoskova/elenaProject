using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class BlogPostAnswersController: BaseController
    {
        public BlogPostAnswersController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public ActionResult Index()
        {
            //return View(Enumerable.Empty<UsersClass>());
            return View(db.BlogPostAnswersObj.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            BlogPostAnswers answerClass = db.BlogPostAnswersObj.Find(id);
            if (answerClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(answerClass);
        }

        // GET: Customer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public async Task<ActionResult> CreateAsync()
        {

            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "id,parent_id,reply,root_post,usersID")] BlogPostAnswers answerClass)
        {
            if (ModelState.IsValid)
            {
                db.BlogPostAnswersObj.Add(answerClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(answerClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            BlogPostAnswers answerClass = db.BlogPostAnswersObj.Find(id);
            if (answerClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }

            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                if (answerClass.usersid != customerClass.id)
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            return View(answerClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "id,parent_id,reply,root_post,usersID")] BlogPostAnswers answerClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answerClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answerClass);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            BlogPostAnswers answerClass = db.BlogPostAnswersObj.Find(id);
            if (answerClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }

            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                if (answerClass.usersid != customerClass.id)
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            return View(answerClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPostAnswers answerClass = db.BlogPostAnswersObj.Find(id);
            db.BlogPostAnswersObj.Remove(answerClass);
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
