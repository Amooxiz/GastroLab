using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class AddressVM
    {
        public int Id { get; set; }
        public string Street { get; set; }
        [DisplayName("House number")]
        public int HouseNumber { get; set; }
        [DisplayName("Flat number")]
        public int? FlatNumber { get; set; }
        public string City { get; set; }
        [DisplayName("Postal code")]
        public string PostCode { get; set; }
    }
}
