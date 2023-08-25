using GastroLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageOrders()
        {
            ViewData.Model = _orderService.GetAllOrders();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            ViewBag.Products = _productService.GetAllProducts();
            return View();
        }

        //[HttpPost]
        //public IActionResult AddOrder()
        //{
            
        //}
    }
}
