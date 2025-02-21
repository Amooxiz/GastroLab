using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public int OrderProductId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        [DisplayName("Product status")]
        public ProductStatus productStatus { get; set; }
        public int Quantity { get; set; } = 1;

        public List<CategoryVM> categories { get; set; } = new List<CategoryVM>();
        public List<IngredientVM> ingredients { get; set; } = new List<IngredientVM>();
        public List<OptionSetVM> optionSets { get; set; } = new List<OptionSetVM>();
        public List<OrderVM> orders { get; set; } = new List<OrderVM>();

        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
        public List<int> SelectedIngredientIds { get; set; } = new List<int>();
        public List<int> SelectedOptionSetIds { get; set; } = new List<int>();

        public string? SerializedOptionSets { get; set; } = string.Empty;
        public string? GlobalOptionSetIds { get; set; } = string.Empty;

        public List<OrderProductOptionVM> OrderOptions { get; set; } = new List<OrderProductOptionVM>();

    }
}
