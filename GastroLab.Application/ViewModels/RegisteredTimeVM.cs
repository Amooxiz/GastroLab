using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class TimeSlotVM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? UserId { get; set; }
    }
}
