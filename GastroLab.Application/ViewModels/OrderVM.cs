using GastroLab.Domain.Models;

namespace GastroLab.Application.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string? TableNr { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Comment { get; set; }
        public int AddressId { get; set; }
        public AddressVM? Address { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public string? ClientId { get; set; }
        public List<OrderProductOptionVM> options { get; set; } = new List<OrderProductOptionVM>();
        public List<ProductVM> products { get; set; } = new List<ProductVM>();
    }
}