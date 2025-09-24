using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers //namespace for controllers
{
    [Authorize] //ensures only authenticated users can access actions in this controller
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()  //displays welcome message based on user role
        {
            var user = _userManager.GetUserAsync(User).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            string roleMessage = roles.Count > 0 ? $"Welcome {roles[0]}" : "Welcome User";
            return Content(roleMessage);
        } 

        [Authorize(Roles = "Admin")] //restricts access to users with Admin role
        public IActionResult AdminPage() => Content("Welcome Admin");

        [Authorize(Roles = "Manager")]
        public IActionResult ManagerPage() => Content("Welcome Manager");

        [Authorize(Roles = "Employee")]
        public IActionResult EmployeePage() => Content("Welcome Employee");
    }
}
