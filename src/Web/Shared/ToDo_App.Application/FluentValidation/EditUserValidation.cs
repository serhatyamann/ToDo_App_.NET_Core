using FluentValidation;
using ToDo_App.Application.Models.AppUser;

namespace ToDo_App.Application.FluentValidation
{
    public class EditUserValidation : AbstractValidator<UpdateProfileModel>
    {
        public EditUserValidation()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty()
               .WithMessage("Name can't be empty")
               .MinimumLength(3)
               .MaximumLength(50)
               .WithMessage("Min 3, max 50 character");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("Name can't be empty")
               .MinimumLength(3)
               .MaximumLength(50)
               .WithMessage("Min 3, max 50 character");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Name can't be empty")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Min 3, max 50 character");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Enter an email address")
                .EmailAddress()
                .WithMessage("Enter a valid a email address");
        }
    }
}
