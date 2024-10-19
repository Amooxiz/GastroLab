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
        [Key]
        public int OrderProductId { get; set; }
        public OrderProduct OrderProduct { get; set; }

        [Key, Column(Order = 1)]
        public int OptionSetId { get; set; }
        public OptionSet OptionSet { get; set; }

        [Key, Column(Order = 2)]
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
