using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Models
{
    public class OptionSet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OptionSetOption>? OptionSetOptions { get; set; }
        public virtual ICollection<ProductOptionSet>? ProductOptionSets { get; set; }
    }
}
