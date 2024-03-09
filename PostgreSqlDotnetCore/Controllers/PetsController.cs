using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;
using System;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class PetsController : BaseController
    {
        public PetsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public async Task<ActionResult> IndexAsync()
        {
            // check for permission
            UsersClass customerClass = await getCrrentUser();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (customerClass.role_id == RoleConstants.Standard)
            {
                // kco
                            var query = from st in db.PetsObj
                            where st.usersid == customerClass.id
                            select st;

                var userPets =
                    //db.PetsObj.FromSql($"SELECT * FROM pets where usersid={customerClass.id}").ToListAsync();
                    await query.ToListAsync<PetsClass>();
                return View(userPets);
            } else
            {
                return View(db.PetsObj.ToList());
            }

        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            PetsClass peClass = db.PetsObj.Find(id);
            if (peClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(peClass);
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
        public async Task<ActionResult> CreateAsync([Bind(include: "id,color,description,dateofbirthday, usersid,typeofpetsid")] PetsClass peClass)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (ModelState.IsValid)
            {
               // peClass.dateofbirthday = DateTime.SpecifyKind(peClass.dateofbirthday, DateTimeKind.Utc);
                var user = await _userManager.GetUserAsync(User);
                var customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                peClass.usersid = customerClass.id;
                // voa go pisav tuka na 18.02
               // PetsClass.dateofbirthday = DateOnly.FromDateTime(DateTime.UtcNow);
                db.PetsObj.Add(peClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(peClass);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            PetsClass peClass = db.PetsObj.Find(id);
            if (peClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(peClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(include: "id,color,description,dateofbirthday, usersid,typeofpetsid")] PetsClass peClass)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
             
            if (ModelState.IsValid)
            {
                //peClass.dateofbirthday = DateTime.SpecifyKind(peClass.dateofbirthday, DateTimeKind.Utc);
                
                var user = await _userManager.GetUserAsync(User);
                var customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                peClass.usersid = customerClass.id;
                db.Entry(peClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(peClass);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            PetsClass peClass = db.PetsObj.Find(id);
            if (peClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(peClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetsClass peClass = db.PetsObj.Find(id);
            db.PetsObj.Remove(peClass);
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
