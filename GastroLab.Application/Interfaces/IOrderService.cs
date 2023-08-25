using GastroLab.Application.ViewModels;
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
    }
}
