using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.Category
{
    public class CreateCategoryModel
    {
        public string AppUserId { get; set; }

        [Required(ErrorMessage ="Category name can't be empty!")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        public string Slug => Name.ToLower().Replace(" ", "_");
        public DateTime CreateDate => DateTime.Now;
        public Status Status { get => Status.Active; }
    }
}
