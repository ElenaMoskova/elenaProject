using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;
using System.Diagnostics;

namespace PostgreSqlDotnetCore.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<IdentityUser> _userManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    UsersClass customerClass = db.CustomerObj.SingleOrDefault(x=> x.email == user.Email);
                    if (customerClass == null)
                    {
                        string[] nameLastName = user.Email.ToString().Split('@');
                        string name = nameLastName[0];
                        string lastName = "-";
                        try
                        {
                            if (nameLastName[0].Contains('.'))
                            {
                                name = nameLastName[0].Split('.')[0];
                                lastName = nameLastName[0].Split('.')[1];
                            }
                        } catch(Exception ex) { 
                        }
                        db.CustomerObj.Add(new UsersClass(
                            user.Email,
                            name,
                            lastName,
                            user.PasswordHash != null ? user.PasswordHash : "-",
                            user.PhoneNumber != null ? user.PhoneNumber : user.Email,
                            RoleConstants.Standard,
                            null
                            )
                        );
                        db.SaveChanges();
                    }

                }

            }
            ViewBag.ShowTopBar = true;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}