using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
