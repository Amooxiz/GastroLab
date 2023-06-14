using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Models
{
    public class OptionSetOption
    {
        public int Id { get; set; }
        public int OptionSetId { get; set; }
        public virtual OptionSet OptionSet { get; set; }
        public int OptionId { get; set; }
        public virtual Option Option { get; set; }
    }
}
