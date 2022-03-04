using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Reminder;

namespace ToDo_App.Application.FluentValidation
{
    public class UpdateReminderValidation : AbstractValidator<UpdateReminderModel>
    {
        public UpdateReminderValidation()
        {
            RuleFor(x => x.CategoryId)
              .NotEmpty()
              .WithMessage("You have to choose a category, please create a category first!");

            RuleFor(x => x.Title)
               .NotEmpty()
               .WithMessage("Title can't be empty")
               .MinimumLength(3)
               .MaximumLength(50)
               .WithMessage("Min 3, max 50 character");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty")
                .MinimumLength(3)
                .MaximumLength(700)
                .WithMessage("Min 10, max 700 character");

            RuleFor(x => x.Importance)
                .NotEmpty()
                .WithMessage("Importance level can't be empty");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .WithMessage("Due date can't be empty");
        }
    }
}
