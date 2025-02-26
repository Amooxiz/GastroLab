﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public virtual ICollection<OptionSetOption>? OptionSetOptions { get; set; }
        public virtual ICollection<ProductOptionSetOption>? ProductOptionSetOptions { get; set; }
        public virtual ICollection<OrderProductOption>? OrderProductOptions { get; set; }
    }
}
