﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}