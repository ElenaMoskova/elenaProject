using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Controllers;
using PostgreSqlDotnetCore.Models;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class JobsController : BaseController
    {
        public JobsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public ActionResult Index()
        {
            //return View(Enumerable.Empty<UsersClass>());
            return View(db.JobsObj.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            JobsClass jobClass = db.JobsObj.Find(id);
            if (jobClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(jobClass);
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
        public async Task<ActionResult> CreateAsync([Bind(include: "id,description,predictedsalery, vetcentersid")] JobsClass jobClass)
        {
            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (ModelState.IsValid)
            {
                db.JobsObj.Add(jobClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");

            }
            JobsClass jobClass = db.JobsObj.Find(id);
            if (jobClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(jobClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(include: "id,description,predictedsalery, vetcentersid")] JobsClass jobClass)
        {
            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(jobClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobClass);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            JobsClass jobClass = db.JobsObj.Find(id);
            if (jobClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(jobClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobsClass jobClass = db.JobsObj.Find(id);
            db.JobsObj.Remove(jobClass);
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
