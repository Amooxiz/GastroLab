using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class Order
    {
        public int Id { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string? TableNr { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Comment { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public TimeSpan? WaitingTime { get; set; }
        public bool isScheduledDelivery { get; set; }
        public DateTime? ScheduledDeliveryDate { get; set; }

        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
