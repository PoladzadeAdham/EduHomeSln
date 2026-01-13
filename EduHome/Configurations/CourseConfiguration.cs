using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHome.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasMany(x => x.Teachers).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);

        }
    }
}
