using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers 
{
    public class AccountController : Controller//controller for managing user accounts
    {
        private readonly UserManager<IdentityUser> _userManager; //manges CRUD operations for user accounts
        private readonly SignInManager<IdentityUser> _signInManager;//handles user sign-in and sign-out processes

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)//constructor with dependency injection
        {
            _userManager = userManager;
            _signInManager = signInManager; //dependency injection
        }

        [HttpGet]
        public IActionResult Login() => View(); //renders login view

        [HttpPost]
        public IActionResult Login(string email, string password) 
        {
            var result = _signInManager.PasswordSignInAsync(email, password, false, false).Result;//attempts to sign in user with provided credentials

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");//redirects to home page on successful login

            ViewBag.Error = "Invalid login";
            return View();  //rerenders login view with error message
        }

        [HttpPost]
        public IActionResult Logout() 
        {
            _signInManager.SignOutAsync().Wait();//signs out the current user
            return RedirectToAction("Login"); //redirects to login page after logout
        }
    }
}
