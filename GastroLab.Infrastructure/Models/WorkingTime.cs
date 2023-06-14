using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Models
{
    public class WorkingTime
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public TimeSpan TimeInterval { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
