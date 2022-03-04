using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.EntityConfiguration.Abstract;

namespace ToDo_App.Infrastructure.EntityConfiguration.Concrete
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Slug).IsRequired(true);

            builder.HasMany(x => x.Reminders)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.Categories)
                   .HasForeignKey(x => x.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);
            base.Configure(builder);
        }
    }
}
