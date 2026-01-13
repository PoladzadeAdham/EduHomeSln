using System.Threading.Tasks;
using EduHome.Context;
using EduHome.ViewModel.TeacherViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class TeacherController(AppDbContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var teachers = await context.Teachers.Select(x => new TeacherGetVm
            {
                Id = x.Id,
                FullName = x.FullName,
                Profession = x.Profession,
                ImagePath = x.ImagePath
            })
                .ToListAsync();

            return View(teachers);
        }
    }
}
