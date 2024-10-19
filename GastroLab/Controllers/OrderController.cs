using Azure.Core;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using GastroLab.Domain.Models;
using GastroLab.Presentation.RequestModels;
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
        public IActionResult EditOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.DeliveryMethods = Enum.GetValues(typeof(DeliveryMethod))
                                  .Cast<DeliveryMethod>()
                                  .Select(d => new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = d.ToString()
                                  }).ToList();

            ViewBag.AllProducts = _productService.GetAllProducts();

            return View(order);
        }


        [HttpPost]
        public IActionResult EditOrder(OrderVM order)
        {
            // Recalculate the total price based on the products
            decimal totalPrice = 0;
            foreach (var product in order.products)
            {
                totalPrice += product.Price;
            }
            order.TotalPrice = totalPrice;

            // Update the order in the database
            _orderService.UpdateOrder(order);

            return RedirectToAction("ManageAllOrders");

            // If model state is invalid, reload the view with existing data
            ViewBag.DeliveryMethods = Enum.GetValues(typeof(DeliveryMethod))
                                  .Cast<DeliveryMethod>()
                                  .Select(d => new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = d.ToString()
                                  }).ToList();

            ViewBag.AllProducts = _productService.GetAllProducts();

            return View(order);
        }

        [HttpGet]
        public IActionResult GetProductDetailsWithOptions(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            // Map the product to a DTO to send to the client
            var productDetails = new
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
                optionSets = product.optionSets.Select(os => new
                {
                    id = os.Id,
                    displayName = os.DisplayName,
                    isMultiple = os.IsMultiple,
                    isRequired = os.IsRequired,
                    options = os.options.Select(o => new
                    {
                        id = o.Id,
                        displayName = o.DisplayName,
                        price = o.Price
                    }).ToList()
                }).ToList()
            };

            return Json(productDetails);
        }


        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("DeleteOrder")]
        public IActionResult DeleteOrderConfirmed(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.DeleteOrder(id);
            return RedirectToAction("ManageAllOrders");
        }

        [HttpGet]
        public IActionResult ManageAllOrders()
        {
            ViewData.Model = _orderService.GetAllActiveOrders();
            return View();
        }

        [HttpGet]
        public IActionResult ManageDeliveryOrders()
        {
            ViewData.Model = _orderService.GetDeliveryOrders();
            return View();
        }

        [HttpPost]
        public IActionResult ChangeStatusOfOrder([FromBody] OrderStatusChangeRequest statusChangeRequest)
        {
            if (statusChangeRequest == null || statusChangeRequest.OrderId == 0)
            {
                return Json(new { success = false, message = "Invalid data received." });
            }

            if (!Enum.TryParse(statusChangeRequest.OrderStatus, out OrderStatus parsedStatus))
            {
                return Json(new { success = false, message = "Invalid order status." });
            }

            _orderService.ChangeStatusOfOrder(statusChangeRequest.OrderId, parsedStatus);

            return Json(new { success = true });
        }


        [HttpGet]
        public IActionResult ManageOrders()
        {
            ViewData.Model = _orderService.GetNewAndInProgressOrders();
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
            if (String.IsNullOrEmpty(_cookieService.GetCookie("products")))
            {
                throw new Exception("No products in the cart");
            }
            var products = JsonConvert.DeserializeObject<CartProductList>(_cookieService.GetCookie("products"));

            if (products == null)
                throw new Exception("Error acured while deserializing cookie");

            foreach (var product in products.Products)
            {
                order.TotalPrice += product.Price * product.Quantity;

                var productVM = new ProductVM()
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    Price = product.Price,
                    Quantity = product.Quantity
                };

                foreach (var option in product.SelectedOptions)
                {
                    var orderProductOptionVM = new OrderProductOptionVM()
                    {
                        OptionSet = new OptionSetVM()
                        {
                            Id = option.OptionSetId,
                            DisplayName = option.OptionSetName
                        },
                        Option = new OptionVM()
                        {
                            Id = option.OptionId,
                            DisplayName = option.OptionName,
                            Price = option.Price
                        }
                    };
                    productVM.OrderOptions.Add(orderProductOptionVM);
                }

                order.products.Add(productVM);
            }

            _orderService.CreateOrder(order);
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
            request.ItemId = Guid.NewGuid();
            productRequestList.Products.Add(request);
            var result = JsonConvert.SerializeObject(productRequestList);
            _cookieService.SetCookie("products", result);
            return Json(new { success = result });
        }

        [HttpPost]
        public IActionResult RemoveProduct(int productId, string itemId)
        {
            var productRequestList = new CartProductList();
            if (!String.IsNullOrEmpty(_cookieService.GetCookie("products")))
            {
                productRequestList = JsonConvert.DeserializeObject<CartProductList>(_cookieService.GetCookie("products"));
                if (productRequestList == null)
                    throw new Exception("Error acured while deserializing cookie");
            }
            var product = productRequestList.Products.FirstOrDefault(p => p.ProductId == productId && p.ItemId == Guid.Parse(itemId));

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
                productPrice = product?.Price,
                updatedCart = result,
                totalPrice = productRequestList.TotalPrice.ToString("F2")
            });
        }
    }
}
