using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class AddressVM : IValidatableObject
    {
        public int Id { get; set; }

        public string? Street { get; set; }

        [DisplayName("House number")]
        public int? HouseNumber { get; set; }

        [DisplayName("Flat number")]
        public int? FlatNumber { get; set; }

        public string? City { get; set; }

        [DisplayName("Postal code")]
        public string? PostCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                yield return new ValidationResult("City is required for Delivery orders.", new[] { nameof(City) });
            }
            else if (City.Length > 70)
            {
                yield return new ValidationResult("City cannot exceed 70 characters.", new[] { nameof(City) });
            }

            if (string.IsNullOrWhiteSpace(Street))
            {
                yield return new ValidationResult("Street is required for Delivery orders.", new[] { nameof(Street) });
            }
            else if (Street.Length > 70)
            {
                yield return new ValidationResult("Street cannot exceed 70 characters.", new[] { nameof(Street) });
            }

            if (string.IsNullOrWhiteSpace(PostCode))
            {
                yield return new ValidationResult("Postal code is required for Delivery orders.", new[] { nameof(PostCode) });
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(PostCode, @"^\d{2}-\d{3}$"))
                {
                    yield return new ValidationResult("Postal code format must be 'NN-NNN' (e.g., 12-345).", new[] { nameof(PostCode) });
                }
            }

            if (FlatNumber.HasValue && FlatNumber.Value <= 0)
            {
                yield return new ValidationResult("Flat number must be a positive number.", new[] { nameof(FlatNumber) });
            }

            if (HouseNumber == null)
            {
                yield return new ValidationResult("House number is required for Delivery orders.", new[] { nameof(HouseNumber) });
            }
            else if (HouseNumber <= 0)
            {
                yield return new ValidationResult("House number is required and must be a positive number for Delivery orders.", new[] { nameof(HouseNumber) });
            }
        }
    }
}
