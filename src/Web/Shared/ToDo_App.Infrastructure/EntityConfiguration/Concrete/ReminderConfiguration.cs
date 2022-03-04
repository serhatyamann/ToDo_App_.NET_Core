
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.EntityConfiguration.Abstract;

namespace ToDo_App.Infrastructure.EntityConfiguration.Concrete
{
    public class ReminderConfiguration : BaseEntityConfiguration<Reminder>
    {
        public override void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.Importance).IsRequired(true);
            builder.Property(x => x.IsDone).IsRequired(true);
            builder.Property(x => x.DueDate).IsRequired(true);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.AppUserId);

            base.Configure(builder);
        }
    }
}
