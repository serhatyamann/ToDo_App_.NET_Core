using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo_App.Domain.Entities.Interface;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName +" "+ LastName; }
        public string ImagePath { get; set; } = "/images/users/userlogo.jpg";
        public List<Category> Categories { get; set; }
        public List<Reminder> Reminders { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; } = Status.Active;

    }
}
