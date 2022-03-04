using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Reminder;
using ToDo_App.Application.Extensions;
using ToDo_App.Application.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ToDo_App.Web.Controllers
{
    [Authorize]
    public class ReminderController : Controller
    {

        private readonly IReminderService _reminderService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ReminderController(IReminderService reminderService, ICategoryService categoryService, IMapper mapper)
        {
            _reminderService = reminderService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create() => View(new CreateReminderModel() { Categories = await _categoryService.GetCategories(User.GetUserId()) });

        [HttpPost]
        public async Task<IActionResult> Create(CreateReminderModel model)
        {
            if (ModelState.IsValid)
            {
                model.isDone = false;
                model.AppUserId = User.GetUserId();
                await _reminderService.Create(model);
                TempData["SuccessCreateReminder"] = "Reminder has been created successfully!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorCreateReminder"] = "Error while creating a reminder!";
                model.Categories = await _categoryService.GetCategories(User.GetUserId());
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> List() => View(await _reminderService.GetReminders(User.GetUserId()));

        public async Task<IActionResult> CompletedReminders()
        {
            var userId = User.GetUserId();
            return View(await _reminderService.GetCompletedReminders(userId));
        }

        public async Task<IActionResult> TodaysReminders()
        {
            var userId = User.GetUserId();
            var reminders = await _reminderService.GetTodaysReminders(userId);
            return View(reminders);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _reminderService.Delete(id);
            TempData["SuccessDeleteReminder"] = "Reminder has been deleted successfully!";
            return RedirectToAction("CompletedReminders");
        }
        public async Task<IActionResult> DeleteFromUpdatePage(int id)
        {
            await _reminderService.Delete(id);
            TempData["SuccessDeleteReminder"] = "Reminder has been deleted successfully!";
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Complete(int reminderId)
        {
            var reminder = _reminderService.GetById(reminderId);
            reminder.Result.IsDone = true;
            reminder.Result.CompletedDate = DateTime.Now;
            reminder.Result.AppUserId = User.GetUserId();
            var updatedReminder = _mapper.Map<UpdateReminderModel>(reminder.Result);
            await _reminderService.Update(updatedReminder);
            TempData["SuccessCompleteReminder"] = $"You just completed '{updatedReminder.Title}' on '{DateTime.Now}'";
            return RedirectToAction("List");
        }

        public async Task<IActionResult> CompleteFromToday(int reminderId)
        {
            var reminder = _reminderService.GetById(reminderId);
            reminder.Result.IsDone = true;
            reminder.Result.CompletedDate = DateTime.Now;
            reminder.Result.AppUserId = User.GetUserId();
            var updatedReminder = _mapper.Map<UpdateReminderModel>(reminder.Result);
            await _reminderService.Update(updatedReminder);
            TempData["SuccessCompleteTodaysReminder"] = $"You just completed '{updatedReminder.Title}' on '{DateTime.Now}'";
            return RedirectToAction("TodaysReminders");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var reminder = _reminderService.GetById(id);
            reminder.Result.Categories = await _categoryService.GetCategories(User.GetUserId());
            return View(reminder.Result);
        }

        public async Task<IActionResult> Update(UpdateReminderModel model)
        {
            if (ModelState.IsValid)
            {
                model.AppUserId = User.GetUserId();
                TempData["SuccessUpdateReminder"] = "Reminder has been updated successfully!";
                await _reminderService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorUpdateReminder"] = "Error while updating reminder!";
                return View(model);
            }
        }
    }
}
