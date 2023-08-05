using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Desciption { get; set; }
        public LeaveRequestStatus Status { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
