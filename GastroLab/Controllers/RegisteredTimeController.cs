using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GastroLab.Presentation.Controllers
{
    public class RegisteredTimeController : Controller
    {
        private readonly IRegisteredTimeService _registeredTimeService;

        public RegisteredTimeController(IRegisteredTimeService registeredTimeService)
        {
            _registeredTimeService = registeredTimeService;
        }
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
            // Get the user id from the claims
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            registeredTimeVM.UserId = userId;

            _registeredTimeService.RegisterTime(registeredTimeVM);
            return RedirectToAction("Index", "Home");
        }
    }
}
