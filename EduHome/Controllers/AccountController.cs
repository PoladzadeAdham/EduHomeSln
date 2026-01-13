using EduHome.Models;
using EduHome.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace EduHome.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = new()
            {
                Email = vm.Email,
                UserName = vm.UserName,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            await _userManager.AddToRoleAsync(user, "Member");

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index","Home");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if(user is null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user,vm.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });

            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Member"
            });

            return Ok("role created");            

        }

    }
}
