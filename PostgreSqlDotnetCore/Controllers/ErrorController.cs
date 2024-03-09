namespace PostgreSqlDotnetCore.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        // GET: ErrorController
        public IActionResult AccessDenied()
        {
            return View();
        }
        // GET: ErrorController
        public IActionResult NotExist()
        {
            return View();
        }

    }
}
