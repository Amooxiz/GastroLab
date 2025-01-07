using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class OrderFilterVM
    {
        public DeliveryMethod? DeliveryMethod { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public DateTime? CompletionDateFrom { get; set; }
        public DateTime? CompletionDateTo { get; set; }
        public string WaitingTimeOption { get; set; }
        public int? WaitingTimeFrom { get; set; }
        public int? WaitingTimeTo { get; set; }
        public bool IsScheduledDelivery { get; set; }
        public DateTime? ScheduledDeliveryDateFrom { get; set; }
        public DateTime? ScheduledDeliveryDateTo { get; set; }

        //pola do sortowania
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }

        // pola do page'owania
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10; // domyślnie 10 rekordów na stronę
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public List<OrderVM> Orders { get; set; } = new List<OrderVM>();
        public List<int> SelectedProductIds { get; set; } = new List<int>();
    }
}
