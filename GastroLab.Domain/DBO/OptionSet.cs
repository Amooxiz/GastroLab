using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class OptionSet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
        public bool IsMultiple { get; set; } = false;

        public virtual ICollection<OptionSetOption>? OptionSetOptions { get; set; }
        public virtual ICollection<ProductOptionSet>? ProductOptionSets { get; set; }
        public virtual ICollection<ProductOptionSetOption>? ProductOptionSetOptions { get; set; }
        public virtual ICollection<OrderProductOption>? OrderProductOptions { get; set; }
    }
}
