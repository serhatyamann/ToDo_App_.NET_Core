using FluentValidation;
using ToDo_App.Application.Models.AppUser;

namespace ToDo_App.Application.FluentValidation
{
    public class PasswordValidation : AbstractValidator<ChangePasswordModel>
    {
        public PasswordValidation()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Enter your current password!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a new password!");
            RuleFor(x => x.ConfirmPassword).Equal(x=> x.Password).WithMessage("Passwords do not match!");
        }
    }
}
