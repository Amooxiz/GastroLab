using GastroLab.Application.Interfaces;
using GastroLab.Domain.Models;
using GastroLab.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GastroLabDbContext _context;

        public OrderRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void AddProductToOrder(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
        }

        public void RemoveProductFromOrder(OrderProduct orderProduct)
        {
            _context.OrderProducts.Remove(orderProduct);
            _context.SaveChanges();
        }

        public void AddOptionToOrderProduct(OrderProductOption orderProductOption)
        {
            _context.OrderProductOptions.Add(orderProductOption);
            _context.SaveChanges();
        }
    }
}
