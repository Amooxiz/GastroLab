using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GlobalSettingsController : Controller
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public GlobalSettingsController(IGlobalSettingsService globalSettingsService)
        {
            _globalSettingsService = globalSettingsService;
        }

        public IActionResult Index()
        {
            var settings = _globalSettingsService.GetSettings();

            return View(settings);
        }

        [HttpPost]
        public IActionResult Index(GlobalSettingsVM model)
        {
            if (ModelState.IsValid)
            {

                _globalSettingsService.UpdateSettings(model);

                ViewBag.Message = "Global settings updated successfully.";
                return View(model);
            }

            return View(model);
        }
    }
}
