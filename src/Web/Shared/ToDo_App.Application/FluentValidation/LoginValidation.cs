using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.AppUser;

namespace ToDo_App.Application.FluentValidation
{
    public class LoginValidation : AbstractValidator<LoginModel>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please enter your username!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter your password!");
        }
    }
}
