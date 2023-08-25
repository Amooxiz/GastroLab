using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int orderId);
        public void AddProductToOrder(OrderProduct orderProduct);
        public void RemoveProductFromOrder(OrderProduct orderProduct);
        public void AddOptionToOrderProduct(OrderProductOption orderProductOption);
        IEnumerable<Order> GetAllOrders();
    }
}
