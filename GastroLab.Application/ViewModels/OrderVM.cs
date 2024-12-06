using GastroLab.Domain.DBO;
using System.ComponentModel;

namespace GastroLab.Application.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        [DisplayName("Delivery method")]
        public DeliveryMethod DeliveryMethod { get; set; }
        [DisplayName("Table number")]
        public string? TableNr { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Comment { get; set; }
        public int AddressId { get; set; }
        public AddressVM? Address { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [DisplayName("Waiting time (minutes)")]
        public int WaitingTime { get; set; }
        [DisplayName("Scheduled delivery")]
        public bool isScheduledDelivery { get; set; }
        [DisplayName("Scheduled delivery date")]
        public DateTime? ScheduledDeliveryDate { get; set; }

        public string DeliveryMethodName => DeliveryMethod.ToString();
        public string StatusName => Status.ToString();

        public string? ClientId { get; set; }
        public List<ProductVM> products { get; set; } = new List<ProductVM>();
    }
}