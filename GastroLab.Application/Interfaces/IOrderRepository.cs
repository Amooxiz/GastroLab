using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GastroLab.Application.Interfaces
{
    public interface IOrderRepository
    {
        public List<(string Month, double AverageQuantity)> GetAvgQuantityByMonths(DateTime dateFrom, DateTime dateTo);
        public List<(string Month, decimal AverageSales)> GetAvgSalesByMonths(DateTime dateFrom, DateTime dateTo);
        public List<(string DayOfWeek, double AverageQuantity)> GetAvgQuantityByDays(DateTime dateFrom, DateTime dateTo);
        public List<(string DayOfWeek, decimal AverageSales)> GetAvgSalesByDays(DateTime dateFrom, DateTime dateTo);
        public List<(string Comparison, int OrderCount)> GetComparisonOfWaitingTimes(DateTime dateFrom, DateTime dateTo);
        public List<(string HourRange, double AverageQuantityDelivery, double AverageQuantityDineIn, double AverageQuantityPickup)> GetAvgDeliveryMethodQuantityByHours(DateTime dateFrom, DateTime dateTo);
        public List<(string HourRange, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimeByHours(DateTime dateFrom, DateTime dateTo);
        public List<(string HourRange, double AverageQuantity)> GetAvgQuantityByHours(DateTime dateFrom, DateTime dateTo);
        public List<(string HourRange, decimal AverageSales)> GetAvgSalesByHours(DateTime dateFrom, DateTime dateTo);
        public List<(string DeliveryMethod, decimal TotalSales)> GetSalesByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<(string DeliveryMethod, int TotalQuantity)> GetQuantityByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<(string DeliveryMethod, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimesByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<(string Product, int QuantityDelivery, int QuantityDineIn, int QuantityPickup)> GetDeliveryMethodsQuantityByProducts(DateTime dateFrom, DateTime dateTo);
        public List<(string ProductName, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimesByProducts(DateTime dateFrom, DateTime dateTo);
        public List<(string ProductName, int TotalQuantity)> GetQuantityByProducts(DateTime dateFrom, DateTime dateTo);
        public List<(string ProductName, decimal TotalSales)> GetSalesByProducts(DateTime dateFrom, DateTime dateTo);


        public IEnumerable<Order> GetFinishedOrders();
        public void CreateOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int orderId);
        public void AddProductToOrder(OrderProduct orderProduct);
        public void RemoveProductFromOrder(OrderProduct orderProduct);
        public void AddOptionToOrderProduct(OrderProductOption orderProductOption);
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetNewAndInProgressOrders();
        public void ChangeStatusOfOrder(int orderId, OrderStatus orderStatus);
        public IEnumerable<Order> GetDeliveryOrders();
        public IEnumerable<Order> GetAllActiveOrders();
        public Order GetOrderById(int id);
        public void DeleteOrderProduct(OrderProduct existingProduct);
        public void DeleteOrderProductOption(OrderProductOption existingOption);
        public void SaveChanges();
    }
}
