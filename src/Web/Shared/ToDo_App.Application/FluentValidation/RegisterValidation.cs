using FluentValidation;
using ToDo_App.Application.Models.AppUser;

namespace ToDo_App.Application.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterModel>
    {
        public RegisterValidation()
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

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Enter a password");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords don't match");
        }
    }
}
