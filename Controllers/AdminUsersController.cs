using Microsoft.AspNetCore.Mvc;
using Webapp1.Models.ViewModels;
using Webapp1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;
        public AdminUsersController(IUserRepository userRepository,
               UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new Webapp1.Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email
                });
                
            }
            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email, 
            };

            var identityResult = 
                await userManager.CreateAsync(IdentityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    //Gi roller til denne brukeren
                    var roles = new List<string> { "User"};

                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }
                    identityResult = 
                        await userManager.AddToRolesAsync(IdentityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");  
                    }
                  
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is not null)
            {
                var identityResult = await userManager.DeleteAsync(user);
                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }
    }
}