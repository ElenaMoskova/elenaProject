namespace PostgreSqlDotnetCore.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PostgreSqlDotnetCore.Data;
    using PostgreSqlDotnetCore.Models;

    public class BaseController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        public UserManager<IdentityUser> _userManager;

        public BaseController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UsersClass?> getCrrentUser()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            UsersClass customerClass = null;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                if (customerClass == null) // if is not admin or manager NO PERMISSION
                {
                    return null;
                }
                return customerClass;
            }
            else
            {
                return null;

            }
        }

        public async Task<UsersClass?> checkAuthorizationAsync()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            UsersClass customerClass = null;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                if (customerClass == null || (customerClass.role_id != RoleConstants.Admin && customerClass.role_id != RoleConstants.Manager)) // if is not admin or manager NO PERMISSION
                {
                    return null;
                }
                return customerClass;
            }
            else
            {
                return null;

            }
        }

        public async Task<UsersClass?> checkAuthorizationSpecificRoleAsync(int roleId)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            UsersClass customerClass = null;
            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                customerClass = db.CustomerObj.SingleOrDefault(x => x.email == user.Email);
                if (customerClass == null || (customerClass.role_id != roleId)) // if is a specific role
                {
                    return null;
                }
                return customerClass;
            }
            else
            {
                return null;

            }
        }
    }
}
