using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
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
                //var settings = _globalSettingsService.GetSettings();

                //settings.RestaurantName = model.RestaurantName;
                //settings.DefaultDineInWaitingTimeInMinutes = model.DefaultDineInWaitingTimeInMinutes;
                //settings.DefaultDeliveryWaitingTimeInMinutes = model.DefaultDeliveryWaitingTimeInMinutes;

                //settings.AddressVM.Street = model.AddressVM.Street;
                //settings.AddressVM.City = model.AddressVM.City;
                //settings.AddressVM.HouseNumber = model.AddressVM.HouseNumber;
                //settings.AddressVM.FlatNumber = model.AddressVM.FlatNumber;
                //settings.AddressVM.PostCode = model.AddressVM.PostCode;

                _globalSettingsService.UpdateSettings(model);

                ViewBag.Message = "Global settings updated successfully.";
                return View(model);
            }

            return View(model);
        }
    }
}
