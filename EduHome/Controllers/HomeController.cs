using System.Diagnostics;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult Ok()
        {
            return Ok("salam skjbsakjakj");
        }
    }
}
