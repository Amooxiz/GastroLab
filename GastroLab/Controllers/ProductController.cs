using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace GastroLab.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpPost]
        public IActionResult DeleteGlobalOptionSets([FromBody] List<int> optionSetIds)
        {
            var usedOptionSets = _optionSetService.GetUsedOptionSetsByIds(optionSetIds);

            if (usedOptionSets.Any())
            {
                return Json(new { success = false, usedOptionSets = usedOptionSets.Select(u => new { id = u.Id, name = u.Name }) });
            }

            _optionSetService.DeleteGlobalOptionSets(optionSetIds);

            return Json(new { success = true, deletedCount = optionSetIds.Count });
        }


        [HttpGet]
        public IActionResult ManageProductComponents()
        {
            var ingredients = _productService.GetAllIngredients()
                .Select(i => new IngredientVM { Id = i.Id, Name = i.Name }).ToList();
            var categories = _productService.GetAllCategories()
                .Select(c => new CategoryVM { Id = c.Id, Name = c.Name }).ToList();

            var model = new Tuple<IEnumerable<IngredientVM>, IEnumerable<CategoryVM>>(ingredients, categories);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteIngredient(int id)
        {
            _productService.DeleteIngredient(id);
            return RedirectToAction(nameof(ManageProductComponents));
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            _productService.DeleteCategory(id);
            return RedirectToAction(nameof(ManageProductComponents));
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.AllOptionSets = _optionSetService.GetAllOptionSets();
            ViewBag.AllCategories = _productService.GetAllCategories();
            ViewBag.AllIngredients = _productService.GetAllIngredients();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllCategories = _productService.GetAllCategories();
                ViewBag.AllIngredients = _productService.GetAllIngredients();

                return View(model);
            }

            _productService.AddProduct(model);
            return RedirectToAction("ProductList");
        }


        [HttpGet]
        public IActionResult EditProduct([FromRoute] int Id)
        {
            ViewBag.AllCategories = _productService.GetAllCategories();
            ViewBag.AllIngredients = _productService.GetAllIngredients();
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
            var temp = _productService.GetProductById(Id);
            ViewData.Model = _productService.GetProductById(Id);
            return View();
        }

        [HttpGet]
        public IActionResult ProductList(string searchString, int? categoryId)
        {
            var productList = new ProductListVM()
            {
                Products = _productService.GetProducts(searchString, categoryId),
                CategoryId = categoryId,
                SearchString = searchString,
                Categories = _productService.GetAllCategories()
                                .Select(c => new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.Name
                                }).ToList()
            };
            ViewData.Model = productList;
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
        public IActionResult GetAllCategory()
        {
            return Ok(_productService.GetAllCategories());
        }

        [HttpGet]
        public IActionResult GetAllIngredient()
        {
            return Ok(_productService.GetAllIngredients());
        }
    }
}
