using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Teacher : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
