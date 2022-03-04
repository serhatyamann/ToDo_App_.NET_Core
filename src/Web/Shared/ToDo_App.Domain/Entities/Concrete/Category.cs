using System;
using System.Collections.Generic;
using ToDo_App.Domain.Entities.Interface;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Domain.Entities.Concrete
{
    public class Category : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public List<Reminder> Reminders { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
