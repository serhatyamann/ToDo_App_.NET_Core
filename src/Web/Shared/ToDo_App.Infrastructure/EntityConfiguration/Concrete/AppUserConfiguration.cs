
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.EntityConfiguration.Abstract;

namespace ToDo_App.Infrastructure.EntityConfiguration.Concrete
{
    public class AppUserConfiguration : BaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).IsRequired(true);
            builder.Property(x => x.NormalizedUserName).IsRequired(false);

            builder.Property(x => x.FirstName).IsRequired(true);
            builder.Property(x => x.LastName).IsRequired(true);
            builder.Property(x => x.LastName).IsRequired(true);

            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasMany(x => x.Categories)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
