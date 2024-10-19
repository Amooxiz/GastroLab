using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ProductStatus productStatus { get; set; }
        public int ProductPricingId { get; set; }
        public virtual ProductPricing? ProductPricing { get; set; }

        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
        public virtual ICollection<ProductOptionSet>? ProductOptionSets { get; set; }
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
        public virtual ICollection<ProductIngredient>? ProductIngredients { get; set; }
        public virtual ICollection<ProductOptionSetOption>? ProductOptionSetOptions { get; set; }
    }
}
