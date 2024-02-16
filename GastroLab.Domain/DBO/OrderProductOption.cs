using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class OrderProductOption
    {
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        [Key, Column(Order = 2)]
        public int OptionSetId { get; set; }
        public OptionSet OptionSet { get; set; }
        
        [Key, Column(Order = 3)]
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
