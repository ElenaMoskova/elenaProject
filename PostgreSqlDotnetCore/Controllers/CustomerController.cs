using PostgreSqlDotnetCore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PostgreSqlDotnetCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace PostgreSqlDotnetCore.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Customer
        public async Task<ActionResult> IndexAsync()
        {
            UsersClass customerClass = await getCrrentUser();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            if (customerClass.role_id == RoleConstants.Standard)
            {
                
                var query = from st in db.CustomerObj
                            where st.id == customerClass.id
                            select st;

                var userPets =
                    //db.PetsObj.FromSql($"SELECT * FROM pets where usersid={customerClass.id}").ToListAsync();
                    await query.ToListAsync<UsersClass>();
                return View(userPets);
            }
            else
            {
                return View(db.CustomerObj.ToList());
            }
          
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> DetailsAsync(int? id)
        {
           
            if (id == null)
            {

                return RedirectToAction("NotExist", "Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // check for permission
            UsersClass customerClass = await getCrrentUser();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

            customerClass = db.CustomerObj.Find(id);
            if (customerClass == null)
            {
                return RedirectToAction("NotExist", "Error");
            }
            return View(customerClass);
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
        public ActionResult Create([Bind(include: "id,name,lastname,email,password,number,role_id,jobs_id")] UsersClass customerClass)
        {
            if (ModelState.IsValid)
            {
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
                                                                       // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: customerClass.password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                customerClass.password = hashed; // Hash passwords 
                db.CustomerObj.Add(customerClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerClass);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // check for permission
            UsersClass customerClass = await getCrrentUser();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

            customerClass = db.CustomerObj.Find(id);
            if (customerClass == null)
            {
                return View(null);
                //return HttpNotFound();
            }
            return View(customerClass);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(include: "id,name,lastname,email,number,role_id,jobs_id")] UsersClass users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(users);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotExist", "Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersClass customerClass = await getCrrentUser();
            if (customerClass == null)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
            return View(customerClass);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersClass customerClass = db.CustomerObj.Find(id);
            db.CustomerObj.Remove(customerClass);
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
