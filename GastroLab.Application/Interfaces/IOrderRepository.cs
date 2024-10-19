﻿using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
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
