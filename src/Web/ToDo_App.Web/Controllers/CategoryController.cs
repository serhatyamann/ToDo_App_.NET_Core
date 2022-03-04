using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo_App.Application.Extensions;
using ToDo_App.Application.Models.Category;
using ToDo_App.Application.Services.Interface;

namespace ToDo_App.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.AppUserId = User.GetUserId();
                TempData["SuccessCreateCategory"] = "Category has been created successfully!";
                await _categoryService.Create(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorCreateCategory"] = "Error while creating category!";
                return View(model);
            }
        }

        public async Task<IActionResult> List()
        {
            var userId = User.GetUserId();
            return View(await _categoryService.GetCategories(userId));
        }

        public async Task<IActionResult> Update(int id) => View(await _categoryService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.AppUserId = User.GetUserId();
                TempData["Success"] = "The category has been updated..!";
                await _categoryService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The category hasn't been updated..!";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> DeletedCategories()
        {
            var userId = User.GetUserId();
            return View(await _categoryService.GetDeletedCategories(userId));
        }
    }
}
