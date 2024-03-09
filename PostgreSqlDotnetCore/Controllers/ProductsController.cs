using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;
using System.Net;

namespace PostgreSqlDotnetCore.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }


        //// GET: Customer
        //public ActionResult Index()
        //{
        //    //return View(Enumerable.Empty<UsersClass>());
        //    return View(db.ProductObj.ToList());
        //}
        // GET: Customer
        public ActionResult Index(string? searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var products = from s in db.ProductObj

                               where s.name.ToLower().Contains(searchString.ToLower())
                               select s;

                //products = products.Where(s => s.name.Contains(searchString));
                return View(products.ToList());

            }
            return View(db.ProductObj.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            ProductsClass prodClass = db.ProductObj.Find(id);
            if (prodClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(prodClass);
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
            var model = new ProductsClass
            {
                dateadded = DateTime.Now // Поставете го датумот на моменталниот датум
            };
            return View(model);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "id,name,description,price, is_active, dateadded, category")] ProductsClass prodClass)
        {
            if (ModelState.IsValid)
            {
                prodClass.dateadded = new DateTime();
                prodClass.isactive = true;
                db.ProductObj.Add(prodClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            ProductsClass prodClass = db.ProductObj.Find(id);
            if (prodClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(prodClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "id,name,description,price, is_active, dateadded, category")] ProductsClass prodClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodClass);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            UsersClass customerClass = await checkAuthorizationAsync();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            ProductsClass prodClass = db.ProductObj.Find(id);
            if (prodClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(prodClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsClass prodClass = db.ProductObj.Find(id);
            db.ProductObj.Remove(prodClass);
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
