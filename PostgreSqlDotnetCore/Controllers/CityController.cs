using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Controllers;
using PostgreSqlDotnetCore.Models;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class CityController : BaseController
    {
        public CityController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public async Task<ActionResult> IndexAsync()
        {
            // check for permission
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            //return View(Enumerable.Empty<UsersClass>());
            return View(db.CitiesObj.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            CitiesClass cityClass = db.CitiesObj.Find(id);
            if (cityClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(cityClass);
        }

        // GET: Customer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "id,name")] CitiesClass cityClass)
        {
            if (ModelState.IsValid)
            {
                db.CitiesObj.Add(cityClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityClass);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            CitiesClass cityClass = db.CitiesObj.Find(id);
            if (cityClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(cityClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "id,name")] CitiesClass cityClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityClass);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            CitiesClass cityClass = db.CitiesObj.Find(id);
            if (cityClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(cityClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CitiesClass cityClass = db.CitiesObj.Find(id);
            db.CitiesObj.Remove(cityClass);
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
