using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOrderService
    {
        public List<AverageQuantityByMonthVM> GetAvgQuantityByMonths(DateTime dateFrom, DateTime dateTo);
        public List<AverageSalesByMonthVM> GetAvgSalesByMonths(DateTime dateFrom, DateTime dateTo);
        public List<AverageQuantityByDayVM> GetAvgQuantityByDays(DateTime dateFrom, DateTime dateTo);
        public List<AverageSalesByDayVM> GetAvgSalesByDays(DateTime dateFrom, DateTime dateTo);
        public List<WaitingTimeComparisonVM> GetComparisonOfWaitingTimes(DateTime dateFrom, DateTime dateTo);
        public List<AverageDeliveryMethodByHoursVM> GetAvgDeliveryMethodQuantityByHours(DateTime dateFrom, DateTime dateTo);
        public List<AverageWaitingTimeByHoursVM> GetAvgWaitingTimeByHours(DateTime dateFrom, DateTime dateTo);
        public List<AverageQuantityByHoursVM> GetAvgQuantityByHours(DateTime dateFrom, DateTime dateTo);
        public List<AverageSalesByHoursVM> GetAvgSalesByHours(DateTime dateFrom, DateTime dateTo);
        public List<SalesByDeliveryMethodVM> GetSalesByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<QuantityByDeliveryMethodVM> GetQuantityByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<AverageWaitingTimeByDeliveryMethodVM> GetAvgWaitingTimesByDeliveryMethods(DateTime dateFrom, DateTime dateTo);
        public List<DeliveryMethodByProductVM> GetDeliveryMethodsQuantityByProducts(DateTime dateFrom, DateTime dateTo);
        public List<AverageWaitingTimeByProductVM> GetAvgWaitingTimesByProducts(DateTime dateFrom, DateTime dateTo);
        public List<QuantityByProductVM> GetQuantityByProducts(DateTime dateFrom, DateTime dateTo);
        public List<SalesByProductVM> GetSalesByProducts(DateTime dateFrom, DateTime dateTo);

        public IEnumerable<OrderVM> GetFinishedOrders();
        public IEnumerable<OrderVM> GetAllOrders();
        public void CreateOrder(OrderVM order);
        public void UpdateOrder(OrderVM order);
        public void DeleteOrder(int orderId);
        public void AddProductToOrder(int orderId, int productId);
        public void RemoveProductFromOrder(int orderId, int productId);
        public void AddOptionToOrderProduct(int orderId, int productId, int optionSetId, int optionId);
        public IEnumerable<OrderVM> GetNewAndInProgressOrders();
        public void ChangeStatusOfOrder(int orderId, OrderStatus orderStatus);
        public IEnumerable<OrderVM> GetDeliveryOrders();
        public IEnumerable<OrderVM> GetAllActiveOrders();
        public OrderVM GetOrderById(int id);
    }
}
