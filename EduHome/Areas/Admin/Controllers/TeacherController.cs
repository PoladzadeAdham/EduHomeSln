using System.Threading.Tasks;
using EduHome.Context;
using EduHome.Helpers;
using EduHome.Models;
using EduHome.ViewModel.TeacherViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public TeacherController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "img", "teacher");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            await SendCoursesWithViewBag();

            var vm = await _context.Teachers.Select(x => new TeacherUpdateVm
            {
                Id = x.Id,
                FullName = x.FullName,
                CourseId = x.CourseId,
                Profession = x.Profession

            }).FirstOrDefaultAsync(x => x.Id == id);

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(TeacherUpdateVm vm)
        {
            await SendCoursesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image?.CheckType() ?? false)
            {
                return View(vm);
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == vm.Id);

            if (teacher is null)
                return NotFound();

            if(vm.Image is { })
            {
                string folderPath = Path.Combine(_folderPath, teacher.ImagePath);

                if (System.IO.File.Exists(folderPath))
                    System.IO.File.Delete(folderPath);

                string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);
                teacher.ImagePath = uniqueFileName;

            }


            teacher.FullName = vm.FullName;
            teacher.CourseId = vm.CourseId;
            teacher.Profession = vm.Profession;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x=>x.Id == id);

            if (teacher is null)
                return NotFound();

            _context.Teachers.Remove(teacher);
            await  _context.SaveChangesAsync();

            string folderPath = Path.Combine(_folderPath, teacher.ImagePath);

            if (System.IO.File.Exists(folderPath))
                System.IO.File.Delete(folderPath);

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.Select(x => new TeacherGetVm
            {
                Id = x.Id,
                FullName = x.FullName,
                ImagePath = x.ImagePath,
                Profession = x.Profession

            }).ToListAsync();

            return View(teachers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SendCoursesWithViewBag();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateVm vm)
        {
            await SendCoursesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckType())
            {
                return View(vm);
            }

            string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

            Teacher teacher = new()
            {
                FullName = vm.FullName,
                CourseId = vm.CourseId,
                Profession = vm.Profession,
                ImagePath = uniqueFileName
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        private async Task SendCoursesWithViewBag()
        {
            var courses = await _context.Courses.ToListAsync();

            ViewBag.Courses = courses;
        }
    }
}
