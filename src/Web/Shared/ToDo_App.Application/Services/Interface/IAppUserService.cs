using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo_App.Application.Models.AppUser;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Entities.Concrete;

namespace ToDo_App.Application.Services.Interface
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterModel model);
        Task<SignInResult> Login(LoginModel model);
        Task LoginWithGoogle(LoginModel model);
        Task LogOut();
        Task<AppUser> UpdateUser(UpdateProfileModel model);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        Task<UpdateProfileModel> GetById(string id);
        Task<GetAppUserVM> GetUserById(string id);
        Task<AppUser> GetDefault(Expression<Func<AppUser, bool>> expression);
        Task<GetAppUserVM> GetAppUserWithoutId(string id, string userName);
        Task<GetAppUserVM> GetAppUserWithoutIdMail(string id, string eMail);
        Task<GetAppUserVM> GetUserByUserName(string userName);
    }
}
