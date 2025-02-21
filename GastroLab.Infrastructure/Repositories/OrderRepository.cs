using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<(string Month, double AverageQuantity)> GetAvgQuantityByMonths(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(order => order.CompletionDate.HasValue)
                .ToList()
                .GroupBy(order => new { order.CompletionDate.Value.Year, order.CompletionDate.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    AverageQuantity = g
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(
                x => new DateTime(x.Year, x.Month, 1),
                x => x.AverageQuantity);

            var monthsList = new List<DateTime>();
            var startDate = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var endDate = new DateTime(dateTo.Year, dateTo.Month, 1);

            for (var date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                monthsList.Add(date);
            }

            CultureInfo englishCulture = new CultureInfo("en-US");

            var result = monthsList.Select(date => new
            {
                Month = englishCulture.DateTimeFormat.GetMonthName(date.Month) + " " + date.Year,
                AverageQuantity = dataDict.ContainsKey(date) ? dataDict[date] : 0.0
            }).ToList();

            return result.Select(x => (x.Month, x.AverageQuantity)).ToList();
        }

        public List<(string Month, decimal AverageSales)> GetAvgSalesByMonths(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(order => order.CompletionDate.HasValue)
                .ToList()
                .GroupBy(order => new { order.CompletionDate.Value.Year, order.CompletionDate.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    AverageSales = g
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Sum(order => order.TotalPrice))
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(
                x => new DateTime(x.Year, x.Month, 1),
                x => x.AverageSales);

            var monthsList = new List<DateTime>();
            var startDate = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var endDate = new DateTime(dateTo.Year, dateTo.Month, 1);

            for (var date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                monthsList.Add(date);
            }

            CultureInfo englishCulture = new CultureInfo("en-US");

            var result = monthsList.Select(date => new
            {
                Month = englishCulture.DateTimeFormat.GetMonthName(date.Month) + " " + date.Year,
                AverageSales = dataDict.ContainsKey(date) ? dataDict[date] : 0.0m
            }).ToList();

            return result.Select(x => (x.Month, x.AverageSales)).ToList();
        }

        public List<(string DayOfWeek, double AverageQuantity)> GetAvgQuantityByDays(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(x => x.CompletionDate.HasValue)
                .ToList()
                .GroupBy(q => q.CompletionDate.Value.DayOfWeek)
                .Select(q => new
                {
                    DayOfWeekEnum = q.Key,
                    AverageQuantity = q
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(x => x.DayOfWeekEnum, x => x.AverageQuantity);

            var daysOfWeek = new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            CultureInfo englishCulture = new CultureInfo("en-US");

            var result = daysOfWeek.Select(day => new
            {
                DayOfWeek = englishCulture.DateTimeFormat.GetDayName(day),
                AverageQuantity = dataDict.ContainsKey(day) ? dataDict[day] : 0.0
            }).ToList();

            return result.Select(x => (x.DayOfWeek, x.AverageQuantity)).ToList();
        }

        public List<(string DayOfWeek, decimal AverageSales)> GetAvgSalesByDays(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(x => x.CompletionDate.HasValue)
                .ToList()
                .GroupBy(q => q.CompletionDate.Value.DayOfWeek)
                .Select(q => new
                {
                    DayOfWeekEnum = q.Key,
                    AverageSales = q
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Sum(order => order.TotalPrice))
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(x => x.DayOfWeekEnum, x => x.AverageSales);

            var daysOfWeek = new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            CultureInfo englishCulture = new CultureInfo("en-US");

            var result = daysOfWeek.Select(day => new
            {
                DayOfWeek = englishCulture.DateTimeFormat.GetDayName(day),
                AverageSales = dataDict.ContainsKey(day) ? dataDict[day] : 0.0m
            }).ToList();

            return result.Select(x => (x.DayOfWeek, x.AverageSales)).ToList();
        }

        public List<(string Comparison, int OrderCount)> GetComparisonOfWaitingTimes(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var result = query
                .Where(order => order.CompletionDate.HasValue)
                .ToList()
                .GroupBy(x => CompareActualToScheduled(x.CompletionDate.Value, x.CreationDate, x.WaitingTime.Value))
                .Select(g => new
                {
                    Comparison = g.Key ? "<= Expected" : "> Expected",
                    OrderCount = g.Count()
                });

            return result.Select(x => (x.Comparison, x.OrderCount)).ToList();
        }

        private bool CompareActualToScheduled(DateTime completionDate, DateTime creationDate, TimeSpan waitingTime)
        {
            var actualTime = completionDate - creationDate;
            if (actualTime.CompareTo(waitingTime) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<(string HourRange, double AverageQuantityDelivery, double AverageQuantityDineIn, double AverageQuantityPickup)> GetAvgDeliveryMethodQuantityByHours(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(x => x.CompletionDate.HasValue)
                .ToList()
                .GroupBy(q => q.CompletionDate.Value.Hour)
                .Select(q => new
                {
                    Hour = q.Key,
                    AverageQuantityDelivery = q
                        .Where(x => x.DeliveryMethod == DeliveryMethod.Delivery)
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .DefaultIfEmpty(0)
                        .Average(),
                    AverageQuantityDineIn = q
                        .Where(x => x.DeliveryMethod == DeliveryMethod.DineIn)
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .DefaultIfEmpty(0)
                        .Average(),
                    AverageQuantityPickup = q
                        .Where(x => x.DeliveryMethod == DeliveryMethod.Pickup)
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .DefaultIfEmpty(0)
                        .Average()
                });

            var deliveryDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageQuantityDelivery);
            var dineInDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageQuantityDineIn);
            var pickupDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageQuantityPickup);

            var result = Enumerable.Range(0, 24).Select(hour => new
            {
                HourRange = $"{hour:00}:00 - {((hour + 1) % 24):00}:00",
                AverageQuantityDelivery = deliveryDict.ContainsKey(hour) ? deliveryDict[hour] : 0.0,
                AverageQuantityDineIn = dineInDict.ContainsKey(hour) ? dineInDict[hour] : 0.0,
                AverageQuantityPickup = pickupDict.ContainsKey(hour) ? pickupDict[hour] : 0.0
            }).ToList();

            return result.Select(x => (x.HourRange, x.AverageQuantityDelivery, x.AverageQuantityDineIn, x.AverageQuantityPickup)).ToList();
        }

        public List<(string HourRange, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimeByHours(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(x => x.CompletionDate.HasValue && x.WaitingTime.HasValue)
                .ToList()
                .GroupBy(q => q.CompletionDate.Value.Hour)
                .Select(q => new
                {
                    Hour = q.Key,
                    AverageExpectedWaitingTime = q.Average(x => x.WaitingTime.Value.TotalMinutes),
                    AverageActualWaitingTime = q.Average(x => (x.CompletionDate - x.CreationDate).Value.TotalMinutes)
                });

            var expectedDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageExpectedWaitingTime);
            var actualDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageActualWaitingTime);

            var result = Enumerable.Range(0, 24).Select(hour => new
            {
                HourRange = $"{hour:00}:00 - {((hour + 1) % 24):00}:00",
                AverageExpectedWaitingTime = expectedDict.ContainsKey(hour) ? expectedDict[hour] : 0.0,
                AverageActualWaitingTime = actualDict.ContainsKey(hour) ? actualDict[hour] : 0.0
            }).ToList();

            return result.Select(x => (x.HourRange, x.AverageExpectedWaitingTime, x.AverageActualWaitingTime)).ToList();
        }

        public List<(string HourRange, double AverageQuantity)> GetAvgQuantityByHours(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(order => order.CompletionDate.HasValue)
                .ToList()
                .GroupBy(order => order.CompletionDate.Value.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    AverageQuantity = g
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Count())
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageQuantity);

            var result = Enumerable.Range(0, 24).Select(hour => new
            {
                HourRange = $"{hour:00}:00 - {((hour + 1) % 24):00}:00",
                AverageQuantity = dataDict.ContainsKey(hour) ? dataDict[hour] : 0.0
            }).ToList();

            return result.Select(x => (x.HourRange, x.AverageQuantity)).ToList();
        }


        public List<(string HourRange, decimal AverageSales)> GetAvgSalesByHours(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var groupedData = query
                .Where(x => x.CompletionDate.HasValue)
                .ToList()
                .GroupBy(order => order.CompletionDate.Value.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    AverageSales = g
                        .GroupBy(order => order.CompletionDate.Value.Date)
                        .Select(dayGroup => dayGroup.Sum(order => order.TotalPrice))
                        .DefaultIfEmpty(0)
                        .Average()
                });

            var dataDict = groupedData.ToDictionary(x => x.Hour, x => x.AverageSales);

            var result = Enumerable.Range(0, 24).Select(hour => new
            {
                HourRange = $"{hour:00}:00 - {((hour + 1) % 24):00}:00",
                AverageSales = dataDict.ContainsKey(hour) ? dataDict[hour] : 0m
            }).ToList();

            return result.Select(x => (x.HourRange, x.AverageSales)).ToList();
        }

        public List<(string DeliveryMethod, decimal TotalSales)> GetSalesByDeliveryMethods(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var result = query
                .GroupBy(q => q.DeliveryMethod)
                .ToList()
                .Select(q => new
                {
                    DeliveryMethod = q.Key,
                    TotalSales = q.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.TotalSales);

            return result.Select(x => (x.DeliveryMethod.ToString(), x.TotalSales)).ToList();
        }
        public List<(string DeliveryMethod, int TotalQuantity)> GetQuantityByDeliveryMethods(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var result = query
                .ToList()
                .GroupBy(q => q.DeliveryMethod)
                .Select(q => new
                {
                    DeliveryMethod = q.Key,
                    TotalQuantity = q.Count()
                })
                .OrderByDescending(x => x.TotalQuantity);

            return result.Select(x => (x.DeliveryMethod.ToString(), x.TotalQuantity)).ToList();
        }
        public List<(string DeliveryMethod, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimesByDeliveryMethods(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrdersByDates(dateFrom, dateTo);

            var result = query
                .Where(x => x.CompletionDate.HasValue && x.WaitingTime.HasValue)
                .ToList()
                .GroupBy(q => q.DeliveryMethod)
                .Select(q => new
                {
                    DeliveryMethod = q.Key,
                    AverageExpectedWaitingTime = q.Average(x => x.WaitingTime.Value.TotalMinutes),
                    AverageActualWaitingTime = q.Average(x => (x.CompletionDate - x.CreationDate).Value.Minutes),
                })
                .OrderBy(x => x.AverageActualWaitingTime);

            return result.Select(x => (x.DeliveryMethod.ToString(), x.AverageExpectedWaitingTime, x.AverageActualWaitingTime)).ToList();
        }
        public List<(string Product, int QuantityDelivery, int QuantityDineIn, int QuantityPickup)> GetDeliveryMethodsQuantityByProducts(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrderProductsByDates(dateFrom, dateTo);

            var result = query
                .ToList()
                .GroupBy(q => q.Product.Name)
                .Select(q => new
                {
                    ProductName = q.Key,
                    QuantityDelivery = q.Where(x => x.Order.DeliveryMethod == DeliveryMethod.Delivery).Sum(x => x.Quantity),
                    QuantityDineIn = q.Where(x => x.Order.DeliveryMethod == DeliveryMethod.DineIn).Sum(x => x.Quantity),
                    QuantityPickup = q.Where(x => x.Order.DeliveryMethod == DeliveryMethod.Pickup).Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantityDineIn + x.QuantityDelivery + x.QuantityPickup);

            return result.Select(x => (x.ProductName, x.QuantityDelivery, x.QuantityDineIn, x.QuantityPickup)).ToList();
        }
        public List<(string ProductName, double AverageExpectedWaitingTime, double AverageActualWaitingTime)> GetAvgWaitingTimesByProducts(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrderProductsByDates(dateFrom, dateTo).ToList();

            var resultUnGrouped = query
                .Where(x => x.Order.WaitingTime.HasValue && x.Order.CompletionDate.HasValue)
                .ToList();
            var result = resultUnGrouped
                .GroupBy(q => q.Product.Name)
                .Select(q => new
                {
                    ProductName = q.Key,
                    AverageExpectedWaitingTime = q.Average(x => x.Order.WaitingTime.Value.TotalMinutes),
                    AverageActualWaitingTime = q.Average(x => (x.Order.CompletionDate - x.Order.CreationDate).Value.TotalMinutes)
                })
                .OrderBy(x => x.AverageActualWaitingTime)
                .ToList();

            return result.Select(x => (x.ProductName, x.AverageExpectedWaitingTime, x.AverageActualWaitingTime)).ToList();
        }
        public List<(string ProductName, int TotalQuantity)> GetQuantityByProducts(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrderProductsByDates(dateFrom, dateTo);

            var result = query
                .ToList()
                .GroupBy(q => q.Product.Name)
                .Select(q => new
                {
                    ProductName = q.Key,
                    TotalQuantity = q.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity);

            return result.Select(x => (x.ProductName, x.TotalQuantity)).ToList();
        }

        public List<(string ProductName, decimal TotalSales)> GetSalesByProducts(DateTime dateFrom, DateTime dateTo)
        {
            var query = FilterOrderProductsByDates(dateFrom, dateTo);

            var result = query
                .ToList()
                .GroupBy(q => q.Product.Name)
                .Select(q => new
                {
                    ProductName = q.Key,
                    TotalSales = q.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.TotalSales);

            return result.Select(x => (x.ProductName, x.TotalSales)).ToList();

        }

        private IQueryable<OrderProduct> FilterOrderProductsByDates(DateTime dateFrom, DateTime dateTo)
        {
            var query = _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .Where(x => x.Order.Status == OrderStatus.Finished)
                .AsQueryable();

            if (dateFrom != null)
            {
                query = query.Where(op => op.Order.CreationDate >= dateFrom);
            }

            if (dateTo != null)
            {
                query = query.Where(op => op.Order.CreationDate <= dateTo);
            }
            return query;
        }

        private IQueryable<Order> FilterOrdersByDates(DateTime dateFrom, DateTime dateTo)
        {
            var query = _context.Orders.Where(x => x.Status == OrderStatus.Finished).AsQueryable();

            if (dateFrom != null)
            {
                query = query.Where(op => op.CreationDate >= dateFrom);
            }

            if (dateTo != null)
            {
                query = query.Where(op => op.CreationDate <= dateTo);
            }
            return query;
        }

        public IEnumerable<Order> GetFinishedOrders()
        {
            return _context.Orders
                .Where(x => x.Status == OrderStatus.Finished)
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
            return _context.Orders.Where(x => 
            (x.Status == OrderStatus.Done || x.Status == OrderStatus.OnTheWay) 
            && x.DeliveryMethod == DeliveryMethod.Delivery && (!x.isScheduledDelivery 
            || (x.ScheduledDeliveryDate.HasValue ? x.ScheduledDeliveryDate.Value.AddDays(-1) : DateTime.MaxValue) <= DateTime.Now))
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
            var scheduledOrders = _context.Orders
                .Where(o =>
                    o.Status != OrderStatus.Finished &&
                    o.Status != OrderStatus.Canceled &&
                    (
                        !o.isScheduledDelivery ||
                        (o.isScheduledDelivery &&
                         o.ScheduledDeliveryDate.HasValue &&
                         o.ScheduledDeliveryDate.Value <= DateTime.Now.AddDays(1))
                    )
                )
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
                .Include(o => o.Address);

            return scheduledOrders.ToList();
        }


        public void ChangeStatusOfOrder(int orderId, OrderStatus orderStatus)
        {
            var order = _context.Orders.Find(orderId);

            if (order == null) 
            {
                throw new Exception("Order not found");
            }

            order.Status = orderStatus;
            if (orderStatus == OrderStatus.Finished)
                order.CompletionDate = DateTime.Now;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public IEnumerable<Order> GetNewAndInProgressOrders()
        {
            var newAndInProgressOrders = _context.Orders.Where(x =>
                     (x.Status == OrderStatus.New || x.Status == OrderStatus.InProgress)
                     && (
                         !x.isScheduledDelivery
                         || (x.ScheduledDeliveryDate.HasValue && x.ScheduledDeliveryDate.Value <= DateTime.Now.AddHours(24))
                     )
                )
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

            return newAndInProgressOrders;
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
