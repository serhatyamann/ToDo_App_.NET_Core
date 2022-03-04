using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Reminder;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Entities.Concrete;

namespace ToDo_App.Application.Services.Interface
{
    public interface IReminderService
    {
        Task Create(CreateReminderModel model);
        Task Update(UpdateReminderModel model);
        Task Delete(int id);

        Task<UpdateReminderModel> GetById(int id);
        Task<List<GetReminderVM>> GetReminders(string AppUserId);

        Task<Reminder> GetReminderById(int id);
        Task<List<GetReminderVM>> GetCompletedReminders(string id);
        Task<List<GetReminderVM>> GetTodaysReminders(string AppUserId);
    }
}
