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
