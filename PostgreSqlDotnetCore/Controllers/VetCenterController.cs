using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;
using System.Data;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class VetCenterController : BaseController
    {
        public VetCenterController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View(db.VetCentersObj.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            VetCenter vetClass = db.VetCentersObj.Find(id);
            if (vetClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(vetClass);
        }

        // GET: Customer/Create
        public async Task<ActionResult> CreateAsync()
        {
            // check for permission
            UsersClass customerClass = await checkAuthorizationSpecificRoleAsync(RoleConstants.Admin);
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
        public ActionResult Create([Bind(include: "id,name,adress,description,workinghours,phonenumber,latitude,longitude,citiesid")] VetCenter vetClass)
        {
            if (ModelState.IsValid)
            {
                db.VetCentersObj.Add(vetClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vetClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            VetCenter vetClass = db.VetCentersObj.Find(id);
            if (vetClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            // check for permission
            UsersClass customerClass = await checkAuthorizationSpecificRoleAsync(RoleConstants.Admin);
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            return View(vetClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "id,name,adress,description,workinghours,phonenumber,latitude,longitude,citiesid")] VetCenter vetClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vetClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vetClass);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            VetCenter vetClass = db.VetCentersObj.Find(id);
            if (vetClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(vetClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VetCenter vetClass = db.VetCentersObj.Find(id);
            db.VetCentersObj.Remove(vetClass);
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


        // GET: VetCenter/Search
        public ActionResult IndexWithSearch(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var vetCenters = db.VetCentersObj.ToList();
                return View(vetCenters);
            }
            else
            {
                var searchResults = db.VetCentersObj.Where(vc => vc.name.Contains(searchTerm)).ToList();
                return View(searchResults);
            }
        }



    }
}
