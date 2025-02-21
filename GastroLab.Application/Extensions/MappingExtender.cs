using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Extensions
{
    public static class MappingExtender
    {
        public static (int RemainingMinutes, string Color) GetRemainingTime(this OrderVM orderVm)
        {
            var elapsedTime = new TimeSpan();
            var remainingMinutes = new int();
            if (orderVm.isScheduledDelivery)
            {
                elapsedTime = orderVm.ScheduledDeliveryDate.Value - DateTime.Now;
                remainingMinutes = (int)Math.Ceiling(elapsedTime.TotalMinutes);
            }
            else
            {
                elapsedTime = DateTime.Now - orderVm.CreationDate;
                remainingMinutes = (int)Math.Ceiling(orderVm.WaitingTime - elapsedTime.TotalMinutes);
            }

            if (remainingMinutes < 0)
            {
                remainingMinutes = 0;
            }

            string remainingTimeColor;
            switch (remainingMinutes)
            {
                case > 60:
                    remainingTimeColor = "green";
                    break;
                case > 30:
                    remainingTimeColor = "#FFA000";
                    break;
                case > 20:
                    remainingTimeColor = "darkorange";
                    break;
                default:
                    remainingTimeColor = "red";
                    break;
            }

            return (remainingMinutes, remainingTimeColor);
        }

        public static LeaveRequest ToModel(this LeaveRequestVM leaveRequestVM)
        {
            var leaveRequest = new LeaveRequest
            {
                Id = leaveRequestVM.Id,
                DateFrom = leaveRequestVM.DateFrom,
                DateTo = leaveRequestVM.DateTo,
                Desciption = leaveRequestVM.Desciption,
                Feedback = leaveRequestVM.Feedback,
                CreatedOn = leaveRequestVM.CreatedOn,
                ResolvedOn = leaveRequestVM.ResolvedOn,
                Status = leaveRequestVM.Status,
                UserId = leaveRequestVM.UserId
            };
            return leaveRequest;
        }

        public static LeaveRequestVM ToVM(this LeaveRequest leaveRequest)
        {
            var leaveRequestVM = new LeaveRequestVM
            {
                Id = leaveRequest.Id,
                DateFrom = leaveRequest.DateFrom,
                DateTo = leaveRequest.DateTo,
                Desciption = leaveRequest.Desciption,
                Feedback = leaveRequest.Feedback,
                CreatedOn = leaveRequest.CreatedOn,
                ResolvedOn = leaveRequest.ResolvedOn,
                Status = leaveRequest.Status,
                UserId = leaveRequest.UserId,
                UserVM = leaveRequest.User.ToVM()
            };
            return leaveRequestVM;
        }

        public static User ToModel(this CreateUserVM vm)
        {
            if (vm == null)
                return null;

            var user = new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
            };

            return user;
        }

        public static EditUserVM ToEditUserVM(this User user)
        {
            if (user == null)
                return null;

            var editVm = new EditUserVM()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName,
            };
            return editVm;
        }

        public static EditUserVM ToEditUserVM(this UserVM vm)
        {
            if (vm == null)
                return null;

            var editVm = new EditUserVM()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.UserName,
            };
            return editVm;
        }

        public static User ToModel(this EditUserVM vm)
        {
            if (vm == null)
                return null;

            var user = new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
            };

            return user;
        }

        public static User ToModel(this UserVM userVM)
        {
            if (userVM == null)
                return null;

            var user = new User
            {
                Id = userVM.Id,
                UserName = userVM.UserName,
                Email = userVM.Email,
                Name = userVM.Name,
                Surname = userVM.Surname,
            };

            return user;
        }

        public static UserVM ToVM(this User user)
        {
            if (user == null)
                return null;

            var userVM = new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            };

            return userVM;
        }

        public static GlobalSettings ToModel(this GlobalSettingsVM globalSettingsVM)
        {
            var globalSettings = new GlobalSettings()
            {
                Id = globalSettingsVM.Id,
                RestaurantName = globalSettingsVM.RestaurantName,
                AddressId = globalSettingsVM.AddressVM.Id,
                DefaultDineInWaitingTime = TimeSpan.FromMinutes(globalSettingsVM.DefaultDineInWaitingTimeInMinutes),
                DefaultDeliveryWaitingTime = TimeSpan.FromMinutes(globalSettingsVM.DefaultDeliveryWaitingTimeInMinutes),
                Address = globalSettingsVM.AddressVM.ToModel()
            };
            return globalSettings;
        }

        public static GlobalSettingsVM ToVM(this GlobalSettings globalSettings)
        {
            var globalSettingsVM = new GlobalSettingsVM()
            {
                Id = globalSettings.Id,
                RestaurantName = globalSettings.RestaurantName,
                DefaultDineInWaitingTimeInMinutes = (int)globalSettings.DefaultDineInWaitingTime.TotalMinutes,
                DefaultDeliveryWaitingTimeInMinutes = (int)globalSettings.DefaultDeliveryWaitingTime.TotalMinutes,
                AddressVM = globalSettings.Address.ToVM()
            };
            return globalSettingsVM;
        }
        public static ProductVM ToVM(this Product product)
        {
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
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
                productStatus = productVM.productStatus,
                ProductPricing = new ProductPricing { Price = productVM.Price },
                ProductOptionSets = new List<ProductOptionSet>()
            };

            if (productVM.SelectedCategoryIds.Count > 0)
                product.ProductCategories = productVM.SelectedCategoryIds.Select(x => new ProductCategory { CategoryId = x, ProductId = product.Id }).ToList();

            if (productVM.SelectedIngredientIds.Count > 0)
                product.ProductIngredients = productVM.SelectedIngredientIds.Select(x => new ProductIngredient { IngredientId = x, ProductId = product.Id }).ToList();

            if (productVM.GlobalOptionSetIds != null)
            {
                var optionSetIdsToCreate = productVM.GlobalOptionSetIds.Split(",").Select(x => int.Parse(x)).ToList();
                optionSetIdsToCreate
                    .Select(x => new ProductOptionSet { OptionSetId = x, ProductId = product.Id })
                    .ToList()
                    .ForEach(x => product.ProductOptionSets.Add(x));
            }

            if (productVM.SerializedOptionSets != null)
            {
                var optionSetsToCreate = JsonConvert.DeserializeObject<List<OptionSetVM>>(productVM.SerializedOptionSets)
                    ?? new List<OptionSetVM>();

                optionSetsToCreate
                    .Select(x => new ProductOptionSet { OptionSetId = x.Id, OptionSet = x.ToModel(), Product = product, ProductId = product.Id})
                    .ToList()
                    .ForEach (x => product.ProductOptionSets.Add(x));
            }
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
                IsGlobal = optionSet.IsGlobal,
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
                IsGlobal = optionSetVM.IsGlobal
            };
            if (optionSetVM.options != null && optionSetVM.options.Count > 0)
            {
                optionSet.OptionSetOptions = optionSetVM.options
                    .Select(x => new OptionSetOption { OptionId = x.Id, Option = x.ToModel(), OptionSetId = optionSet.Id, OptionSet = optionSet, Price = x.SelectedPrice ?? x.Price }).ToList();

            }
            return optionSet;
        }

        public static OrderVM ToVM(this Order order)
        {
            var orderVM = new OrderVM
            {
                Id = order.Id,
                CompletionDate = order.CompletionDate,
                CreationDate = order.CreationDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                DeliveryMethod = order.DeliveryMethod,
                TableNr = order.TableNr,
                Comment = order.Comment,
                isScheduledDelivery = order.isScheduledDelivery,
                ScheduledDeliveryDate = order.ScheduledDeliveryDate,
            };
            
            if (order.WaitingTime != null)
                orderVM.WaitingTime = (int)((TimeSpan)order.WaitingTime).TotalMinutes;

            if (order.Address != null)
                orderVM.Address = order.Address.ToVM();

            if (order.OrderProducts != null)
                orderVM.products = order.OrderProducts.Select(x => x.toProductVM()).ToList();

            return orderVM;
        }

        public static Order ToModel(this OrderVM orderVM)
        {
            var order = new Order
            {
                Id = orderVM.Id,
                CompletionDate = orderVM.CompletionDate,
                CreationDate = DateTime.Now,
                Status = orderVM.Status,
                TotalPrice = orderVM.TotalPrice,
                DeliveryMethod = orderVM.DeliveryMethod,
                TableNr = orderVM.TableNr,
                Comment = orderVM.Comment,
                isScheduledDelivery = orderVM.isScheduledDelivery,
                ScheduledDeliveryDate = orderVM.ScheduledDeliveryDate,
                WaitingTime = TimeSpan.FromMinutes(orderVM.WaitingTime),
            };

            if (orderVM.DeliveryMethod == DeliveryMethod.Delivery && orderVM.Address != null)
                order.Address = orderVM.Address.ToModel();

            if (orderVM.products != null)
                order.OrderProducts = orderVM.products.Select(x => x.ToOrderProduct()).ToList();

            return order;
        }

        public static OrderProductOption ToModel(this OrderProductOptionVM orderProductOptionVM)
        {
            var model = new OrderProductOption()
            {
                OptionSetId = orderProductOptionVM.OptionSet.Id,
                OptionId = orderProductOptionVM.Option.Id,
            };
            return model;
        }

        public static OrderProductOptionVM ToVM(this OrderProductOption orderProductOption)
        {
            var orderProductOptionVM = new OrderProductOptionVM()
            {
                Option = orderProductOption.Option.ToVM(),
                OptionSet = orderProductOption.OptionSet.ToVM()
            };
            return orderProductOptionVM;
        }

        public static OrderProduct ToOrderProduct(this ProductVM productVM)
        {
            var orderProduct = new OrderProduct()
            {
                Quantity = productVM.Quantity,
                ProductId = productVM.Id,
                TotalPrice = productVM.Price,
            };

            if (productVM.OrderOptions != null)
                orderProduct.OrderProductOptions = productVM.OrderOptions.Select(x => x.ToModel()).ToList();

            return orderProduct;
        }

        public static ProductVM toProductVM(this OrderProduct orderProduct)
        {
            var productVM = new ProductVM()
            {
                Id = orderProduct.ProductId,
                OrderProductId = orderProduct.Id,
                Quantity = orderProduct.Quantity,
                Price = orderProduct.TotalPrice,
                Name = orderProduct.Product.Name,
                Description = orderProduct.Product.Description,
                productStatus = orderProduct.Product.productStatus,
            };

            if (orderProduct.OrderProductOptions != null)
            {
                productVM.OrderOptions = orderProduct.OrderProductOptions.Select(x => x.ToVM()).ToList();
            }

            if (orderProduct.Product.ProductCategories != null)
            {
                productVM.categories = orderProduct.Product.ProductCategories.Select(x => x.Category).Select(x => x.ToVM()).ToList();
            }

            if (orderProduct.Product.ProductIngredients != null)
            {
                productVM.ingredients = orderProduct.Product.ProductIngredients.Select(x => x.Ingredient).Select(x => x.ToVM()).ToList();
            }

            if (orderProduct.Product.ProductOptionSets != null)
            {
                productVM.optionSets = orderProduct.Product.ProductOptionSets.Select(x => x.OptionSet).Select(x => x.ToVM()).ToList();
            }

            return productVM;
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
                HouseNumber = addressVM.HouseNumber ?? 0,
                Street = addressVM.Street,
                PostCode = addressVM.PostCode
            };
            return address;
        }

        public static TimeSlotVM ToVM(this WorkingTime workingTime)
        {
            var timeSlotVM = new TimeSlotVM
            {
                Id = workingTime.Id,
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
                Id = timeSlotVM.Id,
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
                Id = registeredTime.Id,
                DateFrom = registeredTime.DateFrom,
                DateTo = registeredTime.DateTo,
                UserId = registeredTime.UserId,
                Description = registeredTime.Description
            };
            return timeSlotVM;
        }

        public static RegisteredTime ToRegisteredTimeModel(this TimeSlotVM timeSlotVM)
        {
            var workingTime = new RegisteredTime
            {
                Id = timeSlotVM.Id,
                DateFrom = timeSlotVM.DateFrom,
                DateTo = timeSlotVM.DateTo,
                DayOfWeek = (int)timeSlotVM.DateFrom.DayOfWeek == 0 ? 6 : (int)timeSlotVM.DateFrom.DayOfWeek - 1,
                TimeInterval = timeSlotVM.DateTo - timeSlotVM.DateFrom,
                Description = timeSlotVM.Description,
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
