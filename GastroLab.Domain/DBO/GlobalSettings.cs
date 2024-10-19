using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class GlobalSettings
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public TimeSpan DefaultDineInWaitingTime { get; set; }
        public TimeSpan DefaultDeliveryWaitingTime { get; set; }
    }
}
