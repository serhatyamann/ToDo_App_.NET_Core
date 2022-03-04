using System;
using ToDo_App.Domain.Entities.Interface;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Domain.Entities.Concrete
{
    public class Reminder : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Importance Importance { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
