using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public OrderVM GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id).ToVM();
        }

        public IEnumerable<OrderVM> GetDeliveryOrders()
        {
            return _orderRepository.GetDeliveryOrders().Select(x => x.ToVM());
        }

        public IEnumerable<OrderVM> GetAllActiveOrders()
        {
            return _orderRepository.GetAllActiveOrders().Select(x => x.ToVM());
        }

        public void ChangeStatusOfOrder(int orderId, OrderStatus orderStatus)
        {
            _orderRepository.ChangeStatusOfOrder(orderId, orderStatus);
        }

        public IEnumerable<OrderVM> GetAllOrders()
        {
            return _orderRepository.GetAllOrders().Select(x => x.ToVM());
        }

        public IEnumerable<OrderVM> GetNewAndInProgressOrders()
        {
            return _orderRepository.GetNewAndInProgressOrders().Select(x => x.ToVM());
        }

        public void CreateOrder(OrderVM order)
        {
            _orderRepository.CreateOrder(order.ToModel());
        }
        public void UpdateOrder(OrderVM order)
        {
            var existingOrder = _orderRepository.GetOrderById(order.Id);
            this.UpdateOrder(existingOrder, order);
        }

        private void UpdateOrder(Order existingOrder, OrderVM updatedOrder)
        {
            existingOrder.Comment = updatedOrder.Comment;
            existingOrder.DeliveryMethod = updatedOrder.DeliveryMethod;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.TableNr = updatedOrder.TableNr;
            existingOrder.TotalPrice = updatedOrder.TotalPrice;
            existingOrder.WaitingTime = updatedOrder.WaitingTime;
            existingOrder.isScheduledDelivery = updatedOrder.isScheduledDelivery;
            existingOrder.ScheduledDeliveryDate = updatedOrder.ScheduledDeliveryDate;
            if (updatedOrder.DeliveryMethod == DeliveryMethod.Delivery && updatedOrder.Address != null)
            {
                if (existingOrder.Address == null)
                    existingOrder.Address = new Address();

                existingOrder.Address.HouseNumber = updatedOrder.Address.HouseNumber;
                existingOrder.Address.FlatNumber = updatedOrder.Address.FlatNumber;
                existingOrder.Address.Street = updatedOrder.Address.Street;
                existingOrder.Address.City = updatedOrder.Address.City;
                existingOrder.Address.PostCode = updatedOrder.Address.PostCode;
            }

            var existingProducts = existingOrder.OrderProducts != null ? existingOrder.OrderProducts.ToList() : new List<OrderProduct>();




            foreach (var updatedProduct in updatedOrder.products)
            {
                var existingProduct = existingProducts
                    .FirstOrDefault(p => p.Id == updatedProduct.OrderProductId);

                if (existingProduct != null)
                {
                    existingProduct.Quantity = updatedProduct.Quantity;
                    existingProduct.TotalPrice = updatedProduct.Price;

                    UpdateOrderProductOptions(existingProduct, updatedProduct.OrderOptions);
                }
                else
                {
                    var newProduct = new OrderProduct
                    {
                        Quantity = updatedProduct.Quantity,
                        TotalPrice = updatedProduct.Price,
                        OrderId = existingOrder.Id,
                        ProductId = updatedProduct.Id,
                        OrderProductOptions = new List<OrderProductOption>()
                    };

                    if (updatedProduct.OrderOptions != null)
                    {
                        foreach (var optionVM in updatedProduct.OrderOptions)
                        {
                            var newOption = new OrderProductOption
                            {
                                OrderProductId = newProduct.Id, // Ustawione po dodaniu do kolekcji
                                OptionSetId = optionVM.OptionSet.Id,
                                OptionId = optionVM.Option.Id
                            };
                            newProduct.OrderProductOptions.Add(newOption);
                        }
                    }
                    
                    if (existingOrder.OrderProducts == null)
                        existingOrder.OrderProducts = new List<OrderProduct>();

                    existingOrder.OrderProducts.Add(newProduct);
                }
            }

            foreach (var existingProduct in existingProducts)
            {
                if (!updatedOrder.products.Any(p => p.OrderProductId == existingProduct.Id))
                {
                    _orderRepository.DeleteOrderProduct(existingProduct);
                }
            }

            _orderRepository.UpdateOrder(existingOrder);
        }

        private void UpdateOrderProductOptions(OrderProduct existingProduct, List<OrderProductOptionVM> updatedOptions)
        {
            var existingOptions = existingProduct.OrderProductOptions.ToList();

            foreach (var updatedOption in updatedOptions)
            {
                var existingOption = existingOptions
                    .FirstOrDefault(o => o.OptionSetId == updatedOption.OptionSet.Id && o.Option.Id == updatedOption.Option.Id);

                if (existingOption == null)
                {
                    var newOption = new OrderProductOption
                    {
                        OrderProductId = existingProduct.Id,
                        OptionSetId = updatedOption.OptionSet.Id,
                        OptionId = updatedOption.Option.Id
                    };
                    existingProduct.OrderProductOptions.Add(newOption);
                }
            }

            foreach (var existingOption in existingOptions)
            {
                if (updatedOptions.Count > 0 && !updatedOptions.Any(o => o.OptionSet.Id == existingOption.OptionSetId && o.Option.Id == existingOption.OptionId))
                {
                    _orderRepository.DeleteOrderProductOption(existingOption);
                }
            }

        }

        public void DeleteOrder(int orderId)
        {
            _orderRepository.DeleteOrder(orderId);
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
