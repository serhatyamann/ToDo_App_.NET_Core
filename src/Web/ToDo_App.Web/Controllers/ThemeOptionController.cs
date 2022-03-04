using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Application.Extensions;

namespace ToDo_App.Web.Controllers
{
    public class ThemeOptionController : Controller
    {
        private readonly IThemeOptionService _themeOptionService;
        public ThemeOptionController(IThemeOptionService themeOptionService)
        {
            _themeOptionService = themeOptionService;
        }


        public IActionResult ThemeOption()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemeOption(GetThemeOptionsVM model)
        {
            var userId = User.GetUserId();
            model.AppUserId = userId;
            var themeOption = _themeOptionService.GetThemeOptionForUser(userId);
            
            if (themeOption.Result != null)
            {
                themeOption.Result.ThemeColor = model.ThemeColor;
                await _themeOptionService.Update(themeOption.Result);
            }
            else
            {
                await _themeOptionService.Create(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
