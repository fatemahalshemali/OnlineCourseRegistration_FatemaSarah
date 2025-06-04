using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseRegistration_FatemaSarah.Data;
using OnlineCourseRegistration_FatemaSarah.Models.ViewModels;

namespace OnlineCourseRegistration_FatemaSarah.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        #region Configuration
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Users

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.Mobile,
                    City = model.City
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var err in result.Errors)
                {
                    //any error that will happen will return us to the view of the current model
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Roles

        [HttpGet]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View();
            }


            return View(new EditRoleViewModel { RoleId = role.Id, RoleName = role.Name });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return RedirectToAction(nameof(RolesList));
                }

                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return View(model);
        }

        #endregion



    }
}
