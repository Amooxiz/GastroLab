using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GastroLab.Infrastructure.Services;
using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;

namespace GastroLab.Presentation.Controllers
{
    public class OptionSetController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOptionSetService _optionSetService;

        public OptionSetController(IProductService productService, IOptionSetService optionSetService)
        {
            _productService = productService;
            _optionSetService = optionSetService;
        }

        [HttpGet]
        public IActionResult OptionList()
        {
            return View(_optionSetService.GetAllOptions());
        }

        [HttpGet]
        public IActionResult DeleteOption([FromRoute] int Id)
        {
            return View(_optionSetService.GetOption(Id));
        }

        [HttpPost]
        public IActionResult DeleteOption(OptionVM optionVM)
        {
            _optionSetService.DeleteOption(optionVM.Id);
            return RedirectToAction("OptionList");
        }

        [HttpGet]
        public IActionResult EditOption([FromRoute] int Id)
        {
            return View(_optionSetService.GetOption(Id));
        }

        [HttpPost]
        public IActionResult EditOption([FromRoute] int Id, OptionVM option)
        {
            option.Id = Id;
            _optionSetService.UpdateOption(option);
            return RedirectToAction("OptionList");
        }

        //[HttpGet]
        //public IActionResult AddOptionSet()
        //{
        //    ViewBag.AllOptions = _optionSetService.GetAllOptions();
        //    return View();
        //}

        [HttpGet]
        public IActionResult AddOptionSet()
        {
            var viewModel = new OptionSetVM
            {
                options = _optionSetService.GetAllOptions().Select(o => new OptionVM
                {
                    Id = o.Id,
                    Name = o.Name,
                    DisplayName = o.DisplayName,
                    Price = o.Price,
                    IsSelected = false,
                    SelectedPrice = o.Price
                }).ToList()
            };

            return View(viewModel);
        }


        //[HttpPost]
        //public IActionResult AddOptionSet(OptionSetVM optionSet, int[] selectedOptions)
        //{
        //    if (selectedOptions != null)
        //    {
        //        foreach (var optionId in selectedOptions)
        //        {
        //            optionSet.options.Add(new OptionVM()
        //            {
        //                Id = optionId
        //            });
        //        }
        //    }
        //    _optionSetService.AddOptionSet(optionSet);
        //    return RedirectToAction("ManageOptionSets");
        //}

        [HttpPost]
        public IActionResult AddOptionSet(OptionSetVM optionSet)
        {
            if (ModelState.IsValid)
            {
                var selectedOptions = optionSet.options.Where(o => o.IsSelected).ToList();
                if (selectedOptions.Count == 0)
                {
                    throw new Exception("No selected options");
                }
                optionSet.options.RemoveAll(o => !o.IsSelected);
                _optionSetService.AddOptionSet(optionSet);


                return RedirectToAction("ManageOptionSets");
            }

            // Reload options if model state is not valid or if submission fails
            optionSet.options = _optionSetService.GetAllOptions().Select(o => new OptionVM
            {
                Id = o.Id,
                Name = o.Name,
                DisplayName = o.DisplayName,
                Price = o.Price,
                IsSelected = optionSet.options.Any(x => x.Id == o.Id && x.IsSelected),
                SelectedPrice = optionSet.options.FirstOrDefault(x => x.Id == o.Id)?.SelectedPrice ?? o.Price
            }).ToList();

            return View(optionSet);
        }


        [HttpGet]
        public IActionResult EditOptionSet([FromRoute] int Id)
        {
            ViewData.Model = _optionSetService.GetOptionSet(Id);
            return View();
        }

        [HttpPost]
        public IActionResult EditOptionSet([FromRoute] int Id, OptionSetVM optionSet)
        {
            optionSet.Id = Id;
            _optionSetService.UpdateOptionSet(optionSet);
            return RedirectToAction("ManageOptionSets");
        }

        [HttpGet]
        public IActionResult OptionSetDetails([FromRoute] int Id)
        {
            ViewData.Model = _optionSetService.GetOptionSet(Id);
            return View();
        }

        [HttpPost]
        public IActionResult AddOption(OptionVM option)
        {
            _optionSetService.AddOption(option);
            return RedirectToAction("OptionList");
        }
        [HttpGet]
        public IActionResult GetAllOption()
        {
            return Ok(_optionSetService.GetAllOptions());
        }

        [HttpGet]
        public IActionResult ManageOptionSets()
        {
            ViewData.Model = _optionSetService.GetAllOptionSets();
            return View();
        }

        [HttpGet]
        public IActionResult MenageOptions([FromRoute] int Id)
        {
            ViewData.Model = _optionSetService.GetAllOptions();
            return View();
        }

        [HttpGet]
        public IActionResult DeleteOptionSet([FromRoute] int Id)
        {
            ViewData.Model = _optionSetService.GetOptionSet(Id);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteOptionSet(OptionSetVM optionSet)
        {
            _optionSetService.DeleteOptionSet(optionSet.Id);
            return RedirectToAction("ManageOptionSets");
        }

        [HttpGet]
        public IActionResult GetOptions([FromRoute] int Id)
        {
            var product = _productService.GetProductById(Id);
            var optionSets = product.optionSets;

            var result = new
            {
                optionSets = optionSets.Select(os => new
                {
                    os.Id,
                    os.DisplayName,
                    options = os.options.Select(o => new
                    {
                        o.Id,
                        o.DisplayName,
                        o.Price
                    })
                })
            };

            return Json(result);
        }

        [HttpPost]
        public IActionResult RemoveOption(int id, int optionSetId)
        {
            _optionSetService.RemoveOption(id, optionSetId);
            // Implementacja usunięcia opcji
            return RedirectToAction("EditOptionSet", new { id = optionSetId });
        }
        

        [HttpPost]
        public IActionResult SaveOptionSetOption(int optionId, int optionSetId, decimal price)
        {
            // Znajdź i zaktualizuj opcję
            _optionSetService.UpdateOptionSetOption(optionId, optionSetId, price);

            return RedirectToAction("EditOptionSet", new { id = optionSetId });
        }
    }
}
