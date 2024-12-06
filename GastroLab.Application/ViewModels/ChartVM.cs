using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    // Sales by Product
    public class SalesByProductVM
    {
        public string Product { get; set; }
        public decimal TotalSales { get; set; }
    }

    // Quantity by Product
    public class QuantityByProductVM
    {
        public string Product { get; set; }
        public int TotalQuantity { get; set; }
    }

    // Average Waiting Time by Product
    public class AverageWaitingTimeByProductVM
    {
        public string Product { get; set; }
        public double AverageExpectedWaitingTime { get; set; }
        public double AverageActualWaitingTime { get; set; }
    }

    public class DeliveryMethodByProductVM
    {
        public string Product { get; set; }
        public int QuantityDelivery { get; set; }
        public int QuantityDineIn { get; set; }
        public int QuantityPickup { get; set; }
    }

    // Sales by Delivery Method
    public class SalesByDeliveryMethodVM
    {
        public string DeliveryMethod { get; set; }
        public decimal TotalSales { get; set; }
    }

    // Quantity by Delivery Method
    public class QuantityByDeliveryMethodVM
    {
        public string DeliveryMethod { get; set; }
        public int TotalQuantity { get; set; }
    }

    // Average Waiting Time by Delivery Method
    public class AverageWaitingTimeByDeliveryMethodVM
    {
        public string DeliveryMethod { get; set; }
        public double AverageExpectedWaitingTime { get; set; }
        public double AverageActualWaitingTime { get; set; }
    }

    // Average Sales by Hours
    public class AverageSalesByHoursVM
    {
        public string HourRange { get; set; }
        public decimal AverageSales { get; set; }
    }

    // Average Quantity by Hours
    public class AverageQuantityByHoursVM
    {
        public string HourRange { get; set; }
        public double AverageQuantity { get; set; }
    }

    // Average Delivery Method by Hours
    public class AverageDeliveryMethodByHoursVM
    {
        public string HourRange { get; set; }
        public double AverageQuantityDelivery { get; set; }
        public double AverageQuantityDineIn { get; set; }
        public double AverageQuantityPickup { get; set; }
    }

    // Average Waiting Time by Hours
    public class AverageWaitingTimeByHoursVM
    {
        public string HourRange { get; set; }
        public double AverageExpectedWaitingTime { get; set; }
        public double AverageActualWaitingTime { get; set; }
    }

    // Working Hours by User
    public class WorkingHoursByUserVM
    {
        public string User { get; set; }
        public double TotalWorkingHours { get; set; }
        public double TotalRegisteredHours { get; set; }
    }

    // Leave Hours by User
    public class LeaveHoursByUserVM
    {
        public string User { get; set; }
        public double TotalLeaveHours { get; set; }
    }

    // Pie Chart Data
    public class WaitingTimeComparisonVM
    {
        public string Comparison { get; set; } // "<= Expected", "> Expected"
        public int OrderCount { get; set; }
    }

    // Average Sales by Day of Week
    public class AverageSalesByDayVM
    {
        public string DayOfWeek { get; set; }
        public decimal AverageSales { get; set; }
    }

    // Average Quantity by Day of Week
    public class AverageQuantityByDayVM
    {
        public string DayOfWeek { get; set; }
        public double AverageQuantity { get; set; }
    }

    // Average Sales by Month
    public class AverageSalesByMonthVM
    {
        public string Month { get; set; }
        public decimal AverageSales { get; set; }
    }

    // Average Quantity by Month
    public class AverageQuantityByMonthVM
    {
        public string Month { get; set; }
        public double AverageQuantity { get; set; }
    }
}

