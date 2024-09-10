﻿using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
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

            if (product.ProductPricing != null)
                productVM.Price = product.ProductPricing.Price;

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
                productStatus = productVM.productStatus,
                ProductPricing = new ProductPricing { Price = productVM.Price }
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
                DisplayName = optionSet.DisplayName,
                IsRequired = optionSet.IsRequired,
                IsMultiple = optionSet.IsMultiple,
                OptionCount = 0
            };
            if (optionSet.OptionSetOptions != null && optionSet.OptionSetOptions.Count > 0)
            {
                optionSetVM.options = optionSet.OptionSetOptions.Select(x => x.ToVM()).ToList();
                optionSetVM.OptionCount = optionSet.OptionSetOptions.Count;

            }
            return optionSetVM;
        }

        public static OptionSet ToModel(this OptionSetVM optionSetVM)
        {
            var optionSet = new OptionSet
            {
                Id = optionSetVM.Id,
                Name = optionSetVM.Name,
                Description = optionSetVM.Description,
                DisplayName = optionSetVM.DisplayName,
                IsRequired = optionSetVM.IsRequired,
                IsMultiple = optionSetVM.IsMultiple,
            };
            if (optionSetVM.options != null && optionSetVM.options.Count > 0)
                optionSet.OptionSetOptions = optionSetVM.options.Select(x => new OptionSetOption { OptionId = x.Id, OptionSetId = optionSet.Id, Price = x.SelectedPrice ?? x.Price }).ToList();
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
                DeliveryMethod = order.DeliveryMethod,
                TableNr = order.TableNr,
                Comment = order.Comment,
                isScheduledDelivery = order.isScheduledDelivery,
                ScheduledDeliveryDate = order.ScheduledDeliveryDate,
                WaitingTime = order.WaitingTime,
            };

            if (order.Address != null)
                orderVM.Address = order.Address.ToVM();

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
                DeliveryMethod = orderVM.DeliveryMethod,
                TableNr = orderVM.TableNr,
                Comment = orderVM.Comment,
                isScheduledDelivery = orderVM.isScheduledDelivery,
                ScheduledDeliveryDate = orderVM.ScheduledDeliveryDate,
                WaitingTime = orderVM.WaitingTime,
            };

            if (orderVM.Address != null)
                order.Address = orderVM.Address.ToModel();

            return order;
        }

        public static OptionVM ToVM(this Option option)
        {
            var optionVM = new OptionVM
            {
                Id = option.Id,
                Name = option.Name,
                DisplayName = option.DisplayName,
                Price = option.Price
            };
            return optionVM;
        }

        public static Option ToModel(this OptionVM optionVM)
        {
            var option = new Option
            {
                Id = optionVM.Id,
                Name = optionVM.Name,
                DisplayName = optionVM.DisplayName,
                Price = optionVM.Price
            };
            return option;
        }

        public static AddressVM ToVM(this Address address)
        {
            var addressVM = new AddressVM
            {
                Id = address.Id,
                City = address.City,
                FlatNumber = address.FlatNumber,
                HouseNumber = address.HouseNumber,
                Street = address.Street,
                PostCode = address.PostCode
            };
            return addressVM;
        }

        public static Address ToModel(this AddressVM addressVM)
        {
            var address = new Address
            {
                Id = addressVM.Id,
                City = addressVM.City,
                FlatNumber = addressVM.FlatNumber,
                HouseNumber = addressVM.HouseNumber,
                Street = addressVM.Street,
                PostCode = addressVM.PostCode
            };
            return address;
        }

        public static TimeSlotVM ToVM(this WorkingTime workingTime)
        {
            var timeSlotVM = new TimeSlotVM
            {
                DateFrom = workingTime.DateFrom,
                DateTo = workingTime.DateTo,
                UserId = workingTime.UserId
            };
            return timeSlotVM;
        }

        public static WorkingTime ToWorkingTimeModel(this TimeSlotVM timeSlotVM)
        {
            var workingTime = new WorkingTime
            {
                DateFrom = timeSlotVM.DateFrom,
                DateTo = timeSlotVM.DateTo,
                DayOfWeek = (int)timeSlotVM.DateFrom.DayOfWeek == 0 ? 6 : (int)timeSlotVM.DateFrom.DayOfWeek - 1,
                TimeInterval = timeSlotVM.DateTo - timeSlotVM.DateFrom,
                UserId = timeSlotVM.UserId
            };
            return workingTime;
        }

        public static TimeSlotVM ToVM(this RegisteredTime registeredTime)
        {
            var timeSlotVM = new TimeSlotVM
            {
                DateFrom = registeredTime.DateFrom,
                DateTo = registeredTime.DateTo,
                UserId = registeredTime.UserId
            };
            return timeSlotVM;
        }

        public static RegisteredTime ToRegisteredTimeModel(this TimeSlotVM timeSlotVM)
        {
            var workingTime = new RegisteredTime
            {
                DateFrom = timeSlotVM.DateFrom,
                DateTo = timeSlotVM.DateTo,
                DayOfWeek = (int)timeSlotVM.DateFrom.DayOfWeek == 0 ? 6 : (int)timeSlotVM.DateFrom.DayOfWeek - 1,
                TimeInterval = timeSlotVM.DateTo - timeSlotVM.DateFrom,
                UserId = timeSlotVM.UserId
            };
            return workingTime;
        }

        public static OptionVM ToVM(this OptionSetOption optionSetOption)
        {
            var optionVM = new OptionVM
            {
                Id = optionSetOption.Option.Id,
                Name = optionSetOption.Option.Name,
                DisplayName = optionSetOption.Option.DisplayName,
                Price = optionSetOption.Price
            };
            return optionVM;
        }
    }
}
