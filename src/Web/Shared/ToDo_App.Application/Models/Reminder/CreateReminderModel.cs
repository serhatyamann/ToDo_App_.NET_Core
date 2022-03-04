using System;
using System.Collections.Generic;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.Reminder
{
    public class CreateReminderModel
    {
        public string AppUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool isDone { get; set; }
        public Importance Importance { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status { get => Status.Active; }
        public List<GetCategoryVM> Categories { get; set; }
    }
}
