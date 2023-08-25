using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderVM> GetAllOrders()
        {
            return _orderRepository.GetAllOrders().Select(x => x.toVM());
        }

        public void CreateOrder(OrderVM order)
        {

        }
        public void UpdateOrder(OrderVM order)
        {

        }
        public void DeleteOrder(int orderId)
        {

        }
        public void AddProductToOrder(int orderId, int productId)
        {

        }
        public void RemoveProductFromOrder(int orderId, int productId)
        {

        }
        public void AddOptionToOrderProduct(int orderId, int productId, int optionSetId, int optionId)
        {

        }
    }
}
