using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool isSelected { get; set; } = false;
        public virtual ICollection<OptionSetOption>? OptionSetOptions { get; set; }
    }
}
