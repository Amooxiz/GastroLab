using GastroLab.Domain.DBO;

namespace GastroLab.Presentation.RequestModels
{
    public class OrderStatusChangeRequest
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
