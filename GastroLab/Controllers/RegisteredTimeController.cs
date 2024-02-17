using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    public class RegisteredTimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterTime(TimeSlotVM registeredTimeVM)
        {
            return View();
        }
    }
}
