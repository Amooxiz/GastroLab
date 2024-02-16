using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class ProductOptionSetOption
    {
        public decimal? Price { get; set; }
        [Key, Column(Order= 0)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Key, Column(Order= 1)]
        public int OptionSetId { get; set; }
        public virtual OptionSet OptionSet { get; set; }
        [Key, Column(Order= 2)]
        public int OptionId { get; set; }
        public virtual Option Option { get; set; }
    }
}
