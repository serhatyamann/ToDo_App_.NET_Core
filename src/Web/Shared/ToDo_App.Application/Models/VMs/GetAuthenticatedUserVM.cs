using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.Application.Models.VMs
{
    public class GetAuthenticatedUserVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int CompleteCount { get; set; }
        public int IncompleteCount { get; set; }
        public int TotalCategories { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
