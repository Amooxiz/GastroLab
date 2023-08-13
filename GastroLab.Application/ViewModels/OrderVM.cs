using GastroLab.Domain.Models;

namespace GastroLab.Application.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public string? ClientId { get; set; }
    }
}