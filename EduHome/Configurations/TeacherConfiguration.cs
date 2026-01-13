using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHome.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(256);

            builder.Property(x => x.Profession).IsRequired().HasMaxLength(256);

            builder.HasOne(x => x.Course).WithMany(x => x.Teachers).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
