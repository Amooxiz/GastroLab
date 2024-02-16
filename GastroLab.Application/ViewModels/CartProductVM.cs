using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class CartProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<OptionSetVM> optionSets { get; set; } = new List<OptionSetVM>();
    }
}
