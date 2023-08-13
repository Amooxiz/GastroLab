using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Extensions
{
    public static class MappingExtender
    {
        public static ProductVM ToVM(this Product product)
        {
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                productStatus = product.productStatus
            };
            if (product.ProductCategories != null && product.ProductCategories.Count > 0)
                productVM.categories = product.ProductCategories.Select(x => x.Category).Select(x => x.ToVM()).ToList();

            if (product.ProductIngredients != null && product.ProductIngredients.Count > 0)
                productVM.ingredients = product.ProductIngredients.Select(x => x.Ingredient).Select(x => x.ToVM()).ToList();

            if (product.ProductOptionSets != null && product.ProductOptionSets.Count > 0)
                productVM.optionSets = product.ProductOptionSets.Select(x => x.OptionSet).Select(x => x.ToVM()).ToList();

            if (product.OrderProducts != null && product.OrderProducts.Count > 0)
                productVM.orders = product.OrderProducts.Select(x => x.Order).Select(x => x.ToVM()).ToList();

            return productVM;
        }

        public static Product ToModel(this ProductVM productVM)
        {
            var product = new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Description = productVM.Description,
                Image = productVM.Image,
                productStatus = productVM.productStatus
            };

            if (productVM.SelectedCategoryIds.Count > 0)
                product.ProductCategories = productVM.SelectedCategoryIds.Select(x => new ProductCategory { CategoryId = x, ProductId = product.Id }).ToList();

            if (productVM.SelectedIngredientIds.Count > 0)
                product.ProductIngredients = productVM.SelectedIngredientIds.Select(x => new ProductIngredient { IngredientId = x, ProductId = product.Id }).ToList();

            if (productVM.SelectedOptionSetIds.Count > 0)
                product.ProductOptionSets = productVM.SelectedOptionSetIds.Select(x => new ProductOptionSet { OptionSetId = x, ProductId = product.Id }).ToList();

            return product;
        }

        public static CategoryVM ToVM(this Category category)
        {
            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };
            return categoryVM;
        }

        public static Category ToModel(this CategoryVM categoryVM)
        {
            var category = new Category
            {
                Id = categoryVM.Id,
                Name = categoryVM.Name
            };
            return category;
        }

        public static IngredientVM ToVM(this Ingredient ingredient)
        {
            var ingredientVM = new IngredientVM
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
            return ingredientVM;
        }

        public static Ingredient ToModel(this IngredientVM ingredientVM)
        {
            var ingredient = new Ingredient
            {
                Id = ingredientVM.Id,
                Name = ingredientVM.Name
            };
            return ingredient;
        }

        public static OptionSetVM ToVM(this OptionSet optionSet)
        {
            var optionSetVM = new OptionSetVM
            {
                Id = optionSet.Id,
                Name = optionSet.Name,
                Description = optionSet.Description,
                DisplayName = optionSet.DisplayName
            };
            return optionSetVM;
        }

        public static OptionSet ToModel(this OptionSetVM optionSetVM)
        {
            var optionSet = new OptionSet
            {
                Id = optionSetVM.Id,
                Name = optionSetVM.Name,
                Description = optionSetVM.Description,
                DisplayName = optionSetVM.DisplayName
            };
            return optionSet;
        }

        public static OrderVM ToVM(this Order order)
        {
            var orderVM = new OrderVM
            {
                Id = order.Id,
                CompletionDate = order.CompletionDate,
                CreationDate = order.CreationDate,
                ClientId = order.ClientId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
            };
            return orderVM;
        }

        public static Order ToModel(this OrderVM orderVM)
        {
            var order = new Order
            {
                Id = orderVM.Id,
                CompletionDate = orderVM.CompletionDate,
                CreationDate = orderVM.CreationDate,
                ClientId = orderVM.ClientId,
                Status = orderVM.Status,
                TotalPrice = orderVM.TotalPrice,
            };
            return order;
        }

        public static OptionVM ToVM(this Option option)
        {
            var optionVM = new OptionVM
            {
                Id = option.Id,
                Name = option.Name
            };
            return optionVM;
        }

        public static Option ToModel(this OptionVM optionVM)
        {
            var option = new Option
            {
                Id = optionVM.Id,
                Name = optionVM.Name
            };
            return option;
        }
    }
}
