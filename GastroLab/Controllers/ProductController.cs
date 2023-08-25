using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GastroLab.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOptionSetService _optionSetService;

        public ProductController(IProductService productService, IOptionSetService optionSetService)
        {
            _productService = productService;
            _optionSetService = optionSetService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.AllOptionSets = _optionSetService.GetAllOptionSets();
            ViewBag.AllCategories = _productService.GetAllCategories();
            ViewBag.AllIngredients = _productService.GetAllIngredients();
            return View();
        }

        //[HttpPost]
        //public IActionResult AddProduct(string Name, string Description, string Image, ProductStatus productStatus)
        //{
        //    _productService.AddProduct(new Application.ViewModels.ProductVM()
        //    {
        //        Name = Name,
        //        Description = Description,
        //        Image = Image,
        //        productStatus = productStatus
        //    });
        //    return View();
        //}

        [HttpPost]
        public IActionResult AddProduct(ProductVM product)
        {
            _productService.AddProduct(product);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult EditProduct([FromRoute] int Id)
        {
            ViewData.Model = _productService.GetProductById(Id);
            return View();
        }

        [HttpPost]
        public IActionResult EditProduct([FromRoute] int Id, ProductVM product)
        {
            product.Id = Id;
            _productService.UpdateProduct(product);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult DeleteProduct([FromRoute] int Id)
        {
            ViewData.Model = _productService.GetProductById(Id);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(ProductVM product)
        {
            _productService.DeleteProduct(product.Id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult ProductDetails([FromRoute] int Id)
        {
            ViewData.Model = _productService.GetProductById(Id);
            return View();
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            ViewData.Model = _productService.GetAllProducts();
            return View();
        }


        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            _productService.AddCategory(new CategoryVM()
            {
                Name = name
            });
            return Ok();
        }

        [HttpPost]
        public IActionResult AddIngredient(string name)
        {
            _productService.AddIngredient(new IngredientVM()
            {
                Name = name
            });
            return Ok();
        }

        [HttpGet]
        public IActionResult AddOptionSet()
        {
            ViewBag.AllOptions = _optionSetService.GetAllOptions();
            return View();
        }

        [HttpPost]
        public IActionResult AddOptionSet(OptionSetVM optionSet, int[] selectedOptions)
        {
            if (selectedOptions != null)
            {
                foreach (var optionId in selectedOptions)
                {
                    optionSet.options.Add(new OptionVM()
                    {
                        Id = optionId
                    });
                }
            }
            _optionSetService.AddOptionSet(optionSet);
            return RedirectToAction("ManageOptionSets");
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
        public IActionResult AddOption(string name)
        {
            _optionSetService.AddOption(new OptionVM()
            {
                Name = name
            });
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_productService.GetAllCategories());
        }

        [HttpGet]
        public IActionResult GetAllIngredient()
        {
            return Ok(_productService.GetAllIngredients());
        }

        [HttpGet]
        public IActionResult GetAllOptionset()
        {
            return Ok(_optionSetService.GetAllOptionSets());
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

        [HttpPost]
        public IActionResult DeleteOption([FromRoute] int Id)
        {
            _optionSetService.DeleteOption(Id);
            return RedirectToAction("ManageOptionSets");
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
    }
}
