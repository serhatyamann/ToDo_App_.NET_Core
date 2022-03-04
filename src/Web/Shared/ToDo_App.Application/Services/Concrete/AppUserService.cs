using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo_App.Application.Models.AppUser;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Enums;
using ToDo_App.Domain.Repositories.Interface.EntityType;

namespace ToDo_App.Application.Services.Concrete
{
    class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IAppUserRepository appUserRepository,
                              IMapper mapper,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UpdateProfileModel> GetById(string id)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    ConfirmPassword = x.PasswordHash,
                    ImagePath = x.ImagePath,
                    Email = x.Email
                },
                expression: x => x.Id == id && x.Status != Status.Passive);

            var updatedUser = _mapper.Map<UpdateProfileModel>(user);

            return updatedUser;
        }

        public async Task<AppUser> GetDefault(Expression<Func<AppUser, bool>> expression)
        {
            return await _appUserRepository.GetDefault(expression);
        }

        public async Task<GetAppUserVM> GetAppUserWithoutId(string id, string userName)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath,
                    Email = x.Email,
                    CreateDate = x.CreateDate
                },
                expression: x=> x.UserName == userName && x.Id != id);

            return user;
        }

        public async Task<GetAppUserVM> GetAppUserWithoutIdMail(string id, string eMail)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath,
                    Email = x.Email,
                    CreateDate = x.CreateDate
                },
                expression: x => x.Email == eMail && x.Id != id);

            return user;
        }

        public async Task<GetAppUserVM> GetUserById(string id)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath,
                    Email = x.Email,
                    CreateDate = x.CreateDate
                },
                expression: x => x.Id == id && x.Status != Status.Passive);

            return user;
        }


        public async Task<GetAppUserVM> GetUserByUserName(string userName)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath,
                    Email = x.Email,
                    CreateDate = x.CreateDate,
                    DeleteDate = (DateTime)x.DeleteDate,
                    Status = x.Status
                },
                expression: x => x.UserName == userName);

            return user;
        }

        public async Task<SignInResult> Login(LoginModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            return result;
        }

        public Task LoginWithGoogle(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = _mapper.Map<AppUser>(model);

            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.EntropyCrop());
                image.Save("wwwroot/images/users/" + user.Id + model.Image.FileName);
                user.ImagePath = ("/images/users/" + user.Id + model.Image.FileName);
            }
            else
            {
                user.ImagePath = "/images/users/userlogo.jpg";
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<AppUser> UpdateUser(UpdateProfileModel model)
        {
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                if (model.UserName != null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }

                if (model.Email != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }

                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.EntropyCrop());
                    image.Save("wwwroot/images/users/" + user.Id + model.Image.FileName);
                    user.ImagePath = ("/images/users/" + user.Id + model.Image.FileName);
                }
                else
                {
                    user.ImagePath = user.ImagePath;
                }
                user.UpdateDate = model.UpdateDate;
                user.Status = model.Status;
                await _appUserRepository.Update(user);
            }
            return user;
        }


        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {

            var user = await _appUserRepository.GetDefault(x => x.Id == model.UserId);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            
            return result;
        }
    }
}
