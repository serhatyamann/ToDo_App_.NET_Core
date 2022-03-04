using Autofac;
using AutoMapper;
using FluentValidation;
using ToDo_App.Application.AutoMapper;
using ToDo_App.Application.FluentValidation;
using ToDo_App.Application.Models.AppUser;
using ToDo_App.Application.Models.Reminder;
using ToDo_App.Application.Services.Concrete;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Domain.Repositories.Interface.EntityType;
using ToDo_App.Infrastructure.Repositories.Concrete;

namespace ToDo_App.Application.IoC
{
    public class AutoFacResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            base.Load(builder);

            #region Services
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ReminderService>().As<IReminderService>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeOptionService>().As<IThemeOptionService>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReminderRepository>().As<IReminderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeOptionRepository>().As<IThemeOptionRepository>().InstancePerLifetimeScope();
            #endregion

            #region Account Validation
            builder.RegisterType<LoginValidation>().As<IValidator<LoginModel>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterModel>>().InstancePerLifetimeScope();
            builder.RegisterType<EditUserValidation>().As<IValidator<UpdateProfileModel>>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordValidation>().As<IValidator<ChangePasswordModel>>().InstancePerLifetimeScope();
            #endregion

            #region Reminder Validation
            builder.RegisterType<CreateReminderValidation>().As<IValidator<CreateReminderModel>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateReminderValidation>().As<IValidator<UpdateReminderModel>>().InstancePerLifetimeScope();
            #endregion


            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();
            #endregion

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
