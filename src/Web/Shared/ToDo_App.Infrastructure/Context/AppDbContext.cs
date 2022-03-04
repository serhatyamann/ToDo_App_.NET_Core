using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.EntityConfiguration.Concrete;

namespace ToDo_App.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers{ get; set; }
        public DbSet<ThemeOption> ThemeOptions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ReminderConfiguration());
            builder.ApplyConfiguration(new ThemeOptionConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
