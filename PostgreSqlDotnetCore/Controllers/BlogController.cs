using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public async Task<ActionResult> IndexAsync()
        {
            // check for permission
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            //return View(Enumerable.Empty<UsersClass>());
            return View(db.BlogPostControllerObj.ToList());
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return View(null);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostConsultation blogClass = db.BlogPostControllerObj.Find(id);
            if (blogClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            // get answers

            // query
            var query = from st in db.BlogPostAnswersObj
                        where st.blogpostconsultationid == blogClass.id
                        select st;
            //elenaaa
            var blogAnswers = query.ToList();
            blogClass.BlogPostAnswers = blogAnswers;
            return View(blogClass);
        }

        // GET: Customer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            var model = new BlogPostConsultation
            {
                //date_askes = DateTime.Now // Поставете го датумот на моменталниот датум
                // date_askes= DateTime.UtcNow.Date
                date_askes = DateOnly.FromDateTime(DateTime.UtcNow.Date)
            };
            return View(model);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(include: "id,date_askes,title,description,users_id")] BlogPostConsultation blogClass)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = User.Identity.IsAuthenticated;
                if (isAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                    // blogClass.date_askes = new DateTime();
                    // blogClass.date_askes = DateTime.UtcNow;
                    blogClass.date_askes = DateOnly.FromDateTime(DateTime.UtcNow);
                    blogClass.users_id = customerClass.id;
                    db.BlogPostControllerObj.Add(blogClass);
                    db.SaveChanges();
                return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }

            return View(blogClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return View(null);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostConsultation blogClass = db.BlogPostControllerObj.Find(id);
            if (blogClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }

            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {

                bool isAuthenticated = User.Identity.IsAuthenticated;
                if (isAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                    if (blogClass.users_id != customerClass.id)
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                }
            }

            return View(blogClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "id,date_askes,title,description,users_id")] BlogPostConsultation blogClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogClass);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        { 
            if (id == null)
           {
               return View(null);
              //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           BlogPostConsultation blogClass = db.BlogPostControllerObj.Find(id);
            if (blogClass == null)
            {
                return View(null);
               //return HttpNotFound();
            }
            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {

                bool isAuthenticated = User.Identity.IsAuthenticated;
                if (isAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                    if (blogClass.users_id != customerClass.id)
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                }
            }
            return View(blogClass);
        }

        // POST: Customer/Delete/5
     

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPostConsultation blogClass = db.BlogPostControllerObj.Find(id);
            db.BlogPostControllerObj.Remove(blogClass);
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
