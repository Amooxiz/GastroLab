using Azure.Core;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using GastroLab.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GastroLab.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;

        public OrderController(IOrderService orderService, IProductService productService, ICookieService cookieService)
        {
            _orderService = orderService;
            _productService = productService;
            _cookieService = cookieService;
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
            ViewBag.DeliveryMethods = Enum.GetValues(typeof(DeliveryMethod))
                                  .Cast<DeliveryMethod>()
                                  .Select(d => new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = d.ToString()
                                  }).ToList();

            ViewBag.AllProducts = _productService.GetAllProducts();
            if (!String.IsNullOrEmpty(_cookieService.GetCookie("products")))
            {
                var products = JsonConvert.DeserializeObject<CartProductList>(_cookieService.GetCookie("products"));
                ViewBag.AddedProducts = products;
            }
            else
                ViewBag.AddedProducts = null;
            
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(OrderVM order)
        {
            //_orderService.AddOrder(order);
            return RedirectToAction("ManageOrders");
        }

        [HttpPost]
        public IActionResult StoreProduct([FromBody] CartProduct request)
        {
            // Here you can process the request and add the product with options to the order
            // Example: Call a service to handle order updates

            // Assuming AddProductWithOptionsToOrderService is a method in your service
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var productRequestList = new CartProductList();
            if (!String.IsNullOrEmpty(_cookieService.GetCookie("products")))
            {
                productRequestList = JsonConvert.DeserializeObject<CartProductList>(_cookieService.GetCookie("products"));
                if (productRequestList == null)
                    throw new Exception("Error acured while deserializing cookie");
            }
            decimal productPrice = request.Price;
            foreach (var option in request.SelectedOptions)
            {
                productPrice += option.Price;
            }
            productRequestList.TotalPrice += productPrice * request.Quantity;
            request.Price = productPrice;
            productRequestList.Products.Add(request);
            var result = JsonConvert.SerializeObject(productRequestList);
            _cookieService.SetCookie("products", result);
            return Json(new { success = result });
        }

        [HttpPost]
        public IActionResult RemoveProduct([FromBody] int productId)
        {
            var productRequestList = new CartProductList();
            if (!String.IsNullOrEmpty(_cookieService.GetCookie("products")))
            {
                productRequestList = JsonConvert.DeserializeObject<CartProductList>(_cookieService.GetCookie("products"));
                if (productRequestList == null)
                    throw new Exception("Error acured while deserializing cookie");
            }
            var product = productRequestList.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                var productPrice = product.Price;
                

                if (product.Quantity > 1)
                {
                    product.Quantity--;
                    productRequestList.TotalPrice -= productPrice;
                }
                else
                {
                    productRequestList.Products.Remove(product);
                    productRequestList.TotalPrice -= productPrice;
                }
            }



            var result = JsonConvert.SerializeObject(productRequestList);
            _cookieService.SetCookie("products", result);
            return Json(new
            {
                success = true,
                productName = product?.ProductName,
                updatedCart = result,
                totalPrice = productRequestList.TotalPrice.ToString("F2")
            });
        }
    }
}
