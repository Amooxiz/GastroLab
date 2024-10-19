using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class OrderProductOptionVM
    {
        //public int ProductId { get; set; }
        public OptionSetVM OptionSet { get; set; }
        public OptionVM Option { get; set; }
    }
}
