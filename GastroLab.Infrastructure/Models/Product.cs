using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ProductStatus productStatus { get; set; }

        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
        public virtual ICollection<ProductOptionSet>? ProductOptionSets { get; set; }
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
        public virtual ICollection<ProductIngredient>? ProductIngredients { get; set; }
    }
}
