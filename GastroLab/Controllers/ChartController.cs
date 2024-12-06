using GastroLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public ChartController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet("SalesByProduct")]
        public IActionResult GetSalesByProduct(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetSalesByProducts(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("QuantityByProduct")]
        public IActionResult GetQuantityByProduct(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetQuantityByProducts(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageWaitingTimeByProduct")]
        public IActionResult GetAverageWaitingTimeByProduct(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgWaitingTimesByProducts(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("DeliveryMethodByProduct")]
        public IActionResult GetDeliveryMethodByProduct(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetDeliveryMethodsQuantityByProducts(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("SalesByDeliveryMethod")]
        public IActionResult GetSalesByDeliveryMethod(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetSalesByDeliveryMethods(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("QuantityByDeliveryMethod")]
        public IActionResult GetQuantityByDeliveryMethod(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetQuantityByDeliveryMethods(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageWaitingTimeByDeliveryMethod")]
        public IActionResult GetAverageWaitingTimeByDeliveryMethod(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgWaitingTimesByDeliveryMethods(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageSalesByHours")]
        public IActionResult GetAverageSalesByHours(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgSalesByHours(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageQuantityByHours")]
        public IActionResult GetAverageQuantityByHours(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgQuantityByHours(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageWaitingTimeByHours")]
        public IActionResult GetAverageWaitingTimeByHours(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgWaitingTimeByHours(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageDeliveryMethodByHours")]
        public IActionResult GetAverageDeliveryMethodByHours(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgDeliveryMethodQuantityByHours(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("WaitingTimeComparison")]
        public IActionResult GetComparisonOfWaitingTimes(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetComparisonOfWaitingTimes(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageSalesByDays")]
        public IActionResult GetAverageSalesByDays(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgSalesByDays(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageQuantityByDays")]
        public IActionResult GetAverageQuantityByDays(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgQuantityByDays(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageSalesByMonths")]
        public IActionResult GetAverageSalesByMonths(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgSalesByMonths(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("AverageQuantityByMonths")]
        public IActionResult GetAverageQuantityByMonths(DateTime dateFrom, DateTime dateTo)
        {
            var data = _orderService.GetAvgQuantityByMonths(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("WorkingHoursByUsers")]
        public IActionResult GetWorkingHoursByUsers(DateTime dateFrom, DateTime dateTo)
        {
            var data = _userService.GetWorkingHoursByUsers(dateFrom, dateTo);
            return Ok(data);
        }

        [HttpGet("LeaveHoursByUsers")]
        public IActionResult GetLeaveHoursByUsers(DateTime dateFrom, DateTime dateTo)
        {
            var data = _userService.GetLeaveHoursByUsers(dateFrom, dateTo);
            return Ok(data);
        }
    }
}
