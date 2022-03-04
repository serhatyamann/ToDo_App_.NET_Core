using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_App.Application.Extensions;
using System.Threading.Tasks;
using ToDo_App.Application.Models.AppUser;
using ToDo_App.Application.Services.Interface;
using Microsoft.AspNetCore.Identity;
using ToDo_App.Domain.Entities.Concrete;
using System.Security.Claims;
using ToDo_App.Domain.Enums;
using System;

namespace ToDo_App.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;
        public AccountController(IAppUserService appUserService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._appUserService = appUserService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);

                if (userName == null)
                {
                    var result = await _appUserService.Register(model);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This username is already in use!");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _appUserService.GetUserByUserName(model.UserName);
                if (user.Result != null)
                {
                    if (user.Result.Status == Status.Passive)
                    {
                        TempData["AccountDeletion"] = "Your account has been deactivated. To reactivate, send a request to admin@mail.com.!";
                        return View(model);
                    }
                    else
                    {
                        var result = await _appUserService.Login(model);

                        if (result.Succeeded)
                        {
                            return RedirectToLocal(returnUrl);
                        }

                        ModelState.AddModelError(string.Empty, "Invalid User!");
                    }
                }
                else
                {
                    var result = await _appUserService.Login(model);

                    if (result.Succeeded)
                    {
                        return RedirectToLocal(returnUrl);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid User!");
                }

            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _appUserService.LogOut();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        public async Task<IActionResult> Edit(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _appUserService.GetById(User.GetUserId());

                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileModel model)
        {

            if (ModelState.IsValid)
            {
                model.Id = User.GetUserId();
                var user = await _appUserService.GetById(User.GetUserId());
                if (user.UserName == model.UserName & user.Email == model.Email)
                {
                    var updatedUser = await _appUserService.UpdateUser(model);
                    await _userManager.UpdateAsync(updatedUser);
                    await _signInManager.RefreshSignInAsync(updatedUser);

                    TempData["SuccessUpdate"] = "Profile updated!";
                    return View(model);
                }
                else if (user.UserName != model.UserName & user.Email == model.Email)
                {
                    var userNameUser = await _appUserService.GetAppUserWithoutId(model.Id, model.UserName);
                    if (userNameUser != null)
                    {
                        ModelState.AddModelError("", "This username is already in use!");
                        return View(model);
                    }
                    else
                    {
                        var updatedUser = await _appUserService.UpdateUser(model);
                        await _userManager.UpdateAsync(updatedUser);
                        await _signInManager.RefreshSignInAsync(updatedUser);

                        TempData["SuccessUpdate"] = "Profile updated!";
                        return View(model);
                    }
                }
                else if (user.UserName == model.UserName & user.Email != model.Email)
                {
                    var userNameEMail = await _appUserService.GetAppUserWithoutIdMail(model.Id, model.Email);
                    if (userNameEMail != null)
                    {
                        ModelState.AddModelError("", "This e-mail address is already in use!");
                        return View(model);
                    }
                    else
                    {
                        var updatedUser = await _appUserService.UpdateUser(model);
                        await _userManager.UpdateAsync(updatedUser);
                        await _signInManager.RefreshSignInAsync(updatedUser);

                        TempData["SuccessUpdate"] = "Profile updated!";
                        return View(model);
                    }
                }
                else
                {
                    var userNameUser = await _appUserService.GetAppUserWithoutId(model.Id, model.UserName);
                    var userNameEMail = await _appUserService.GetAppUserWithoutIdMail(model.Id, model.Email);
                    if (userNameUser == null & userNameEMail == null)
                    {
                        var updatedUser = await _appUserService.UpdateUser(model);
                        await _userManager.UpdateAsync(updatedUser);
                        await _signInManager.RefreshSignInAsync(updatedUser);

                        TempData["SuccessUpdate"] = "Profile updated!";
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please enter a username and email address that does not exist in the system!");
                        return View(model);
                    }
                }
            }
            else
            {
                TempData["Error"] = "Error while saving changes!";
            }
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            var userHasPassword = await _userManager.HasPasswordAsync(user);

            if (!userHasPassword)
            {
                return RedirectToAction("AddPassword");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(User.GetUserId());
                if (user != null)
                {
                    model.UserId = User.GetUserId();
                    var result = await _appUserService.ChangePassword(model);

                    if (result.Succeeded)
                    {
                        TempData["ChangePasswordSuccess"] = "Your password is successfully changed!";
                        await _signInManager.RefreshSignInAsync(user);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        TempData["ChangePasswordError"] = "Error while changing password!";
                        return View();
                    }
                }
            }
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    TempData["ErrorAddPassword"] = "Error while setting a password!";
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);

                TempData["SuccessAddPassword"] = "Your password has been created successfully!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };

            var existedUser = await _appUserService.GetDefault(x => x.Email == userInfo[1]);

            if (existedUser == null)
            {
                await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

                AppUser user = new AppUser
                {
                    FirstName = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    Status = Status.Active,
                    ImagePath = "/images/users/userlogo.jpg",
                    CreateDate = DateTime.Now
                };

                IdentityResult identityResult = await _userManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    identityResult = await _userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }

            }
            else if (existedUser.Status == Status.Passive)
            {
                TempData["AccountDeletion"] = "Your account has been deactivated. To reactivate, send a request to admin@mail.com.!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                await _signInManager.SignInAsync(existedUser, false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction("Login", "Account");
        }


        public async Task<ActionResult> DeleteUser()
        {

            if (User.GetUserId() == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(User.GetUserId());
            user.Status = Status.Passive;
            user.DeleteDate = DateTime.Now;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            TempData["AccountDeletion"] = "Your account has been deactivated. To reactivate, send a request to admin@mail.com.!";
            return RedirectToAction("Login", "Account");
        }
    }
}
