using AutoMapper;
using ToDo_App.Application.Models.AppUser;
using ToDo_App.Application.Models.Category;
using ToDo_App.Application.Models.Reminder;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Entities.Concrete;

namespace ToDo_App.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CreateCategoryModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();
            CreateMap<Category, GetCategoryVM>().ReverseMap();
            CreateMap<UpdateCategoryModel, GetCategoryVM>().ReverseMap();

            CreateMap<AppUser, RegisterModel>().ReverseMap();
            CreateMap<AppUser, LoginModel>().ReverseMap();
            CreateMap<AppUser, UpdateProfileModel>().ReverseMap();
            CreateMap<GetAppUserVM, UpdateProfileModel>().ReverseMap();
            CreateMap<UpdateProfileModel, AppUser>().ReverseMap();

            CreateMap<Reminder, CreateReminderModel>().ReverseMap();
            CreateMap<Reminder, UpdateReminderModel>().ReverseMap();
            CreateMap<Reminder, GetReminderVM>().ReverseMap();
            CreateMap<GetReminderVM, GetReminderVM>().ReverseMap();
            CreateMap<UpdateReminderModel, GetReminderVM>().ReverseMap();
            CreateMap<GetReminderVM, UpdateReminderModel>().ReverseMap();
            CreateMap<UpdateReminderModel, Reminder>().ReverseMap();


            CreateMap<GetThemeOptionsVM, ThemeOption>().ReverseMap();
            CreateMap<ThemeOption, GetThemeOptionsVM>().ReverseMap();

        }
    }
}
