using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.Models
{
    public class OptionSet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public virtual ICollection<OptionSetOption>? OptionSetOptions { get; set; }
        public virtual ICollection<ProductOptionSet>? ProductOptionSets { get; set; }
    }
}
