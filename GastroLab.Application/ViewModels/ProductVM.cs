﻿using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public int OrderProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
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

        public string SerializedOptionSets { get; set; } = string.Empty;
        public string GlobalOptionSetIds { get; set; } = string.Empty;

        public List<OrderProductOptionVM> OrderOptions { get; set; } = new List<OrderProductOptionVM>();

    }
}
