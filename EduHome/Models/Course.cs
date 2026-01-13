using EduHome.Models.Common;

namespace EduHome.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public ICollection<Teacher> Teachers { get; set; }

    }
}
