using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModel.TeacherViewModel
{
    public class TeacherUpdateVm
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public string Profession { get; set; }
        public int CourseId { get; set; }
    }
}
