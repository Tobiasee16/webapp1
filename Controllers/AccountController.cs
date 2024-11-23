using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webapp1.Models.ViewModels;

namespace Webapp1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager <IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                //Assign this user to "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");    

                if (roleIdentityResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }   
            //show error message
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, 
            loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {

                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return RedirectToPage(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            //show error message
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
