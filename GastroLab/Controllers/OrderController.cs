using Azure.Core;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using GastroLab.Domain.Models;
using GastroLab.Presentation.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace GastroLab.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;
        private readonly IGlobalSettingsService _globalSettingsService;

        public OrderController(IOrderService orderService, IProductService productService, ICookieService cookieService, IGlobalSettingsService globalSettingsService)
        {
            _orderService = orderService;
            _productService = productService;
            _cookieService = cookieService;
            _globalSettingsService = globalSettingsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Director,Waiter")]
        public JsonResult GetOrderDetails(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found." });
            }

            return Json(order);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Director,Waiter")]
        public IActionResult GetHistoricalOrders(OrderFilterVM filter)
        {
            // If no filter is provided, initialize a new one
            if (filter == null)
            {
                filter = new OrderFilterVM();
            }

            if (string.IsNullOrEmpty(filter.SortColumn))
            {
                filter.SortColumn = "CreationDate";
                filter.SortDirection = "desc";
            }

            // Retrieve all orders
            var orders = _orderService.GetFinishedOrders();

            // Apply filters
            if (filter.DeliveryMethod.HasValue)
            {
                orders = orders.Where(o => o.DeliveryMethod == filter.DeliveryMethod.Value).ToList();
            }

            if (filter.CreationDateFrom.HasValue)
            {
                orders = orders.Where(o => o.CreationDate >= filter.CreationDateFrom.Value).ToList();
            }

            if (filter.CreationDateTo.HasValue)
            {
                orders = orders.Where(o => o.CreationDate <= filter.CreationDateTo.Value).ToList();
            }

            if (filter.CompletionDateFrom.HasValue)
            {
                orders = orders.Where(o => o.CompletionDate.HasValue && o.CompletionDate.Value >= filter.CompletionDateFrom.Value).ToList();
            }

            if (filter.CompletionDateTo.HasValue)
            {
                orders = orders.Where(o => o.CompletionDate.HasValue && o.CompletionDate.Value <= filter.CompletionDateTo.Value).ToList();
            }

            if (!string.IsNullOrEmpty(filter.WaitingTimeOption))
            {
                if (filter.WaitingTimeOption == "custom" && filter.WaitingTimeFrom.HasValue && filter.WaitingTimeTo.HasValue)
                {
                    orders = orders.Where(o => o.WaitingTime >= filter.WaitingTimeFrom
                        && o.WaitingTime <= filter.WaitingTimeTo).ToList();
                }
                else
                {
                    switch (filter.WaitingTimeOption)
                    {
                        case "20-30":
                            orders = orders.Where(o => o.WaitingTime >= 20 && o.WaitingTime < 30).ToList();
                            break;
                        case "30-45":
                            orders = orders.Where(o => o.WaitingTime >= 30 && o.WaitingTime < 45).ToList();
                            break;
                        case "45-60":
                            orders = orders.Where(o => o.WaitingTime >= 45 && o.WaitingTime < 60).ToList();
                            break;
                        case "60-90":
                            orders = orders.Where(o => o.WaitingTime >= 60 && o.WaitingTime < 90).ToList();
                            break;
                        case "90+":
                            orders = orders.Where(o => o.WaitingTime >= 90).ToList();
                            break;
                    }
                }
            }

            if (filter.IsScheduledDelivery)
            {
                orders = orders.Where(o => o.isScheduledDelivery).ToList();

                if (filter.ScheduledDeliveryDateFrom.HasValue)
                {
                    orders = orders.Where(o => o.ScheduledDeliveryDate.HasValue && o.ScheduledDeliveryDate.Value >= filter.ScheduledDeliveryDateFrom.Value).ToList();
                }

                if (filter.ScheduledDeliveryDateTo.HasValue)
                {
                    orders = orders.Where(o => o.ScheduledDeliveryDate.HasValue && o.ScheduledDeliveryDate.Value <= filter.ScheduledDeliveryDateTo.Value).ToList();
                }
            }

            // Filter by selected products
            if (filter.SelectedProductIds != null && filter.SelectedProductIds.Any())
            {
                orders = orders.Where(o => o.products.Any(p => filter.SelectedProductIds.Contains(p.Id))).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(filter.SortColumn))
            {
                switch (filter.SortColumn)
                {
                    case "Id":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.Id).ToList() : orders.OrderByDescending(o => o.Id).ToList();
                        break;
                    case "DeliveryMethod":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.DeliveryMethod).ToList() : orders.OrderByDescending(o => o.DeliveryMethod).ToList();
                        break;
                    case "WaitingTime":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.WaitingTime).ToList() : orders.OrderByDescending(o => o.WaitingTime).ToList();
                        break;
                    case "ActualWaitingTime":
                        orders = (filter.SortDirection == "asc")
                            ? orders.OrderBy(o => o.CompletionDate.HasValue ? (int)(o.CompletionDate.Value - o.CreationDate).TotalMinutes : int.MaxValue).ToList()
                            : orders.OrderByDescending(o => o.CompletionDate.HasValue ? (int)(o.CompletionDate.Value - o.CreationDate).TotalMinutes : int.MinValue).ToList();
                        break;
                    case "CreationDate":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.CreationDate).ToList() : orders.OrderByDescending(o => o.CreationDate).ToList();
                        break;
                    case "CompletionDate":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.CompletionDate).ToList() : orders.OrderByDescending(o => o.CompletionDate).ToList();
                        break;
                    case "ScheduledDeliveryDate":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.ScheduledDeliveryDate).ToList() : orders.OrderByDescending(o => o.ScheduledDeliveryDate).ToList();
                        break;
                    case "TotalPrice":
                        orders = (filter.SortDirection == "asc") ? orders.OrderBy(o => o.TotalPrice).ToList() : orders.OrderByDescending(o => o.TotalPrice).ToList();
                        break;
                    default:
                        // Default sorting by CreationDate descending
                        orders = orders.OrderByDescending(o => o.CreationDate).ToList();
                        break;
                }
            }
            else
            {
                // Default sorting by CreationDate descending
                orders = orders.OrderByDescending(o => o.CreationDate).ToList();
            }

            filter.TotalRecords = orders.Count();

            // Paging
            orders = orders.Skip((filter.PageNumber - 1) * filter.PageSize)
                           .Take(filter.PageSize)
                           .ToList();


            // Map to ViewModel
            filter.Orders = orders.ToList();

            // Populate filter dropdowns and other necessary data
            PopulateFilterViewBag();

            return View(filter);
        }

        private void PopulateFilterViewBag()
        {
            ViewBag.DeliveryMethods = Enum.GetValues(typeof(DeliveryMethod))
                                  .Cast<DeliveryMethod>()
                                  .Select(d => new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = d.ToString()
                                  }).ToList();

            ViewBag.WaitingTimeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "All" },
                new SelectListItem { Value = "20-30", Text = "20-30 min" },
                new SelectListItem { Value = "30-45", Text = "30-45 min" },
                new SelectListItem { Value = "45-60", Text = "45-60 min" },
                new SelectListItem { Value = "60-90", Text = "60-90 min" },
                new SelectListItem { Value = "90+", Text = "90+ min" },
                new SelectListItem { Value = "custom", Text = "Custom" }
            };

            // Add this line to pass all products to the view
            ViewBag.AllProducts = _productService.GetAllProducts().ToList();
        }

        [HttpGet]
        [Authorize(Roles = "Waiter,Director,Admin")]
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
        [Authorize(Roles = "Waiter,Director,Admin")]
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
        [Authorize(Roles = "Waiter,Director,Admin")]
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
                    name = os.Name,
                    isMultiple = os.IsMultiple,
                    isRequired = os.IsRequired,
                    options = os.options.Select(o => new
                    {
                        id = o.Id,
                        name = o.Name,
                        price = o.Price
                    }).ToList()
                }).ToList()
            };

            return Json(productDetails);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Director")]
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
        [Authorize(Roles = "Admin,Director,Waiter")]
        public IActionResult ManageAllOrders()
        {
            ViewData.Model = _orderService.GetAllActiveOrders().OrderByDescending(x => x.CreationDate);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Director,Delivery")]
        public IActionResult ManageDeliveryOrders()
        {
            ViewData.Model = _orderService.GetDeliveryOrders().OrderByDescending(x => x.CreationDate);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Director,Delivery,Cook")]
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
        [Authorize(Roles = "Admin,Director,Cook")]
        public IActionResult ManageOrders()
        {
            ViewData.Model = _orderService.GetNewAndInProgressOrders().OrderByDescending(x => x.CreationDate);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Director,Waiter")]
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

            ViewBag.GlobalSettings = _globalSettingsService.GetSettings();
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Director,Waiter")]
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

            _cookieService.RemoveCookie("products");

            return RedirectToAction("ManageAllOrders");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Director,Waiter")]
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
        [Authorize(Roles = "Admin,Director,Waiter")]
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
