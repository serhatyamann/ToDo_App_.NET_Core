using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.EntityConfiguration.Abstract;

namespace ToDo_App.Infrastructure.EntityConfiguration.Concrete
{
    public class ThemeOptionConfiguration : BaseEntityConfiguration<ThemeOption>
    {
        public override void Configure(EntityTypeBuilder<ThemeOption> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ThemeColor).IsRequired(false);
            builder.Property(x => x.AppUserId).IsRequired(false);

            base.Configure(builder);
        }
    }
}
