using System.Threading.Tasks;
using EduHome.Context;
using EduHome.ViewModel.CourseViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class CourseController(AppDbContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var courses = await context.Courses
                                        .Select(x=> new CourseGetVm
                                        {
                                            Id = x.Id,
                                            Title = x.Title,
                                            Description = x.Description,
                                            ImagePath = x.ImagePath
                                        })
                                        .ToListAsync();

            return View(courses);
        }
    }
}
