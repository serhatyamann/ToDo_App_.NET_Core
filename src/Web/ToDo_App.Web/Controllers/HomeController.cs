using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ToDo_App.Web.Models;
using ToDo_App.Application.Extensions;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Application.Models.VMs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ToDo_App.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace ToDo_App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUserService _appUserService;
        private readonly IReminderService _reminderService;
        private readonly ICategoryService _categoryService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IThemeOptionService _themeOptionService;

        public HomeController(ILogger<HomeController> logger,
                              IAppUserService appUserService,
                              IReminderService reminderService,
                              ICategoryService categoryService,
                              SignInManager<AppUser> signInManager,
                              IThemeOptionService themeOptionService)
        {
            _logger = logger;
            _appUserService = appUserService;
            _reminderService = reminderService;
            _categoryService = categoryService;
            _signInManager = signInManager;
            _themeOptionService = themeOptionService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();

            GetAuthenticatedUserVM model = new GetAuthenticatedUserVM();

            if (User.Identity.IsAuthenticated)
            {
                var isUserDeleted = await _appUserService.GetUserById(userId);
                if (isUserDeleted != null)
                {
                    model.ImagePath = "/images/users/userlogo.jpg";
                    if (userId != null)
                    {

                        var user = await _appUserService.GetUserById(userId);
                        if (user.ImagePath == null)
                        {
                            model.ImagePath = "wwwroot/images/users/userlogo.jpg";
                        }
                        model.ImagePath = user.ImagePath;
                        model.Email = user.Email;
                        model.Id = userId;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;
                        model.UserName = user.UserName;
                        model.Password = user.Password;
                        model.CreateDate = user.CreateDate;
                    }
                    var completedReminders = await _reminderService.GetCompletedReminders(userId);
                    var incompleteReminders = await _reminderService.GetReminders(userId);
                    var categories = await _categoryService.GetCategories(userId);
                    model.CompleteCount = completedReminders.Count;
                    model.IncompleteCount = incompleteReminders.Count;
                    model.TotalCategories = categories.Count;
                }
                else
                {
                    TempData["LoginError"] = "Error while logging in! Please re-login!";
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return View(model);
            }
            return View(model);
        }

        public IActionResult Privacy()
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
