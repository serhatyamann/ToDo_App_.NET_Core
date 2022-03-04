using System;
using System.Collections.Generic;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.Reminder
{
    public class UpdateReminderModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GetCategoryVM> Categories { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int MyProperty { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }
        public Importance Importance { get; set; }
        public DateTime? UpdateDate { get => DateTime.Now; }
        public Status Status { get => Status.Modified; }
    }
}
