using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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



        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductIngredients)
                            .ThenInclude(pi => pi.Ingredient)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.OrderProductOptions)
                        .ThenInclude(opo => opo.Option)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductOptionSets)
                            .ThenInclude(pos => pos.OptionSet)
                .Include(o => o.Address)
                .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetDeliveryOrders()
        {
            return _context.Orders
                .Where(x => (x.Status == OrderStatus.Done || x.Status == OrderStatus.OnTheWay) && x.DeliveryMethod == DeliveryMethod.Delivery)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductIngredients)
                            .ThenInclude(pi => pi.Ingredient)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.OrderProductOptions)
                        .ThenInclude(opo => opo.Option)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductOptionSets)
                            .ThenInclude(pos => pos.OptionSet)
                .Include(o => o.Address)
                .ToList();
        }

        public IEnumerable<Order> GetAllActiveOrders()
        {
            return _context.Orders
                .Where(x => x.Status != OrderStatus.Finished)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductIngredients)
                            .ThenInclude(pi => pi.Ingredient)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.OrderProductOptions)
                        .ThenInclude(opo => opo.Option)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductOptionSets)
                            .ThenInclude(pos => pos.OptionSet)
                .Include(o => o.Address)
                .ToList();
        }

        public void ChangeStatusOfOrder(int orderId, OrderStatus orderStatus)
        {
            var order = _context.Orders.Find(orderId);

            if (order == null) 
            {
                throw new Exception("Order not found");
            }

            order.Status = orderStatus;
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public IEnumerable<Order> GetNewAndInProgressOrders()
        {
            return _context.Orders
                .Where(x => x.Status == OrderStatus.New || x.Status == OrderStatus.InProgress)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductIngredients)
                            .ThenInclude(pi => pi.Ingredient)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.OrderProductOptions)
                        .ThenInclude(opo => opo.Option)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductOptionSets)              
                            .ThenInclude(pos => pos.OptionSet)              
                .Include(o => o.Address)
                .ToList();

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

        public void DeleteOrderProduct(OrderProduct existingProduct)
        {
            _context.OrderProducts.Remove(existingProduct);
        }

        public void DeleteOrderProductOption(OrderProductOption existingOption)
        {
            _context.OrderProductOptions.Remove(existingOption);
        }

        public void SaveChanges()
        {
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
