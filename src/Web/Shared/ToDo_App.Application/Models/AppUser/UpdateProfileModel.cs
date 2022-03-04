using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.AppUser
{
    public class UpdateProfileModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
