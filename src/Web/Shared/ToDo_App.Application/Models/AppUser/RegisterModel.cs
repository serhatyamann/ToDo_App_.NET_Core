using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.AppUser
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
