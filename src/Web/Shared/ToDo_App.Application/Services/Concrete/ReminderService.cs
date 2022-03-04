using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Reminder;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Enums;
using ToDo_App.Domain.Repositories.Interface.EntityType;

namespace ToDo_App.Application.Services.Concrete
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;
        public ReminderService(IReminderRepository reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        public async Task Create(CreateReminderModel model)
        {
            var reminder = _mapper.Map<Reminder>(model);
            await _reminderRepository.Add(reminder);
        }

        public async Task Delete(int id)
        {
            var reminder = await _reminderRepository.GetDefault(x => x.Id == id);
            if (reminder != null)
            {
                _reminderRepository.Delete(reminder);
            }
        }

        public async Task Update(UpdateReminderModel model)
        {
            var reminder = _mapper.Map<Reminder>(model);

            await _reminderRepository.Update(reminder);
        }

        public async Task<UpdateReminderModel> GetById(int id)
        {
            var reminder = await _reminderRepository.GetFilteredFirstOrDefault(
                selector: x => new GetReminderVM
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Description = x.Description,
                    Importance = x.Importance,
                    IsDone = x.IsDone,
                    CompletedDate = x.CompletedDate,
                    DueDate = x.DueDate,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    DeleteDate = x.DeleteDate,
                },
                expression: x => x.Id == id && x.Status != Status.Passive);

            var updatedReminder = _mapper.Map<UpdateReminderModel>(reminder);

            return updatedReminder;
        }

        public async Task<Reminder> GetReminderById(int id)
        {
            var reminder = await _reminderRepository.GetDefault(x => x.Id == id);
            return reminder;
        }

        public async Task<List<GetReminderVM>> GetReminders(string AppUserId)
        {
            var reminderList = await _reminderRepository.GetFilteredList(
                selector: x => new GetReminderVM
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Description = x.Description,
                    Importance = x.Importance,
                    DueDate = x.DueDate,
                    CompletedDate = x.CompletedDate,
                    IsDone = x.IsDone,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    DeleteDate = x.DeleteDate,
                },
                expression: x => x.Status != Status.Passive && x.AppUserId == AppUserId && x.IsDone == false );

            return reminderList;
        }

        public async Task<List<GetReminderVM>> GetTodaysReminders(string AppUserId)
        {
            var reminderList = await _reminderRepository.GetFilteredList(
                selector: x => new GetReminderVM
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Description = x.Description,
                    Importance = x.Importance,
                    DueDate = x.DueDate,
                    CompletedDate = x.CompletedDate,
                    IsDone = x.IsDone,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    DeleteDate = x.DeleteDate,
                },
                expression: x => x.Status != Status.Passive && x.AppUserId == AppUserId && x.IsDone == false && x.DueDate.Date == DateTime.Now.Date);

            return reminderList;
        }


        public async Task<List<GetReminderVM>> GetCompletedReminders(string id)
        {
            var reminderList = await _reminderRepository.GetFilteredList(
                selector: x => new GetReminderVM
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Description = x.Description,
                    Importance = x.Importance,
                    DueDate = x.DueDate,
                    IsDone = x.IsDone,
                    CompletedDate = x.CompletedDate,
                    CreateDate = x.CreateDate,
                    DeleteDate = x.DeleteDate,
                },
                expression: x => x.Status != Status.Passive && x.AppUserId == id && x.IsDone == true);

            return reminderList;
        }

    }
}
