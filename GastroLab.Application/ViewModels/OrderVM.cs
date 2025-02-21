using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroLab.Application.ViewModels
{
    public class OrderVM : IValidatableObject
    {
        public int Id { get; set; }

        [DisplayName("Delivery method")]
        public DeliveryMethod DeliveryMethod { get; set; }

        [DisplayName("Table number")]
        public string? TableNr { get; set; }

        public decimal TotalPrice { get; set; }

        [MaxLength(300, ErrorMessage = "Comment cannot exceed 300 characters.")]
        public string? Comment { get; set; }

        public int AddressId { get; set; }
        [ValidateNever]
        public AddressVM Address { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        [DisplayName("Waiting time (minutes)")]
        public int WaitingTime { get; set; }

        [DisplayName("Scheduled delivery")]
        public bool isScheduledDelivery { get; set; }

        [DisplayName("Scheduled delivery date")]
        public DateTime? ScheduledDeliveryDate { get; set; }

        public string DeliveryMethodName => DeliveryMethod.ToString();
        public string StatusName => Status.ToString();

        public List<ProductVM> products { get; set; } = new List<ProductVM>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (products == null || products.Count == 0)
            {
                yield return new ValidationResult("You must add at least one product to the order.", new[] { nameof(products) });
            }


            if (!isScheduledDelivery)
            {
                if (WaitingTime <= 0)
                {
                    yield return new ValidationResult("Waiting time is required and must be a positive number if scheduled delivery is not selected.", new[] { nameof(WaitingTime) });
                }


                if (DeliveryMethod == DeliveryMethod.DineIn)
                {
                    if (string.IsNullOrWhiteSpace(TableNr))
                    {
                        yield return new ValidationResult("Table number is required for DineIn orders when not scheduled.", new[] { nameof(TableNr) });
                    }
                    else if (!int.TryParse(TableNr, out int tableNo) || tableNo <= 0)
                    {
                        yield return new ValidationResult("Table number must be a positive number.", new[] { nameof(TableNr) });
                    }
                }
            }


            if (isScheduledDelivery)
            {
                if (!ScheduledDeliveryDate.HasValue)
                {
                    yield return new ValidationResult("Scheduled delivery date is required if scheduled delivery is selected.", new[] { nameof(ScheduledDeliveryDate) });
                }
                else if (ScheduledDeliveryDate.Value <= DateTime.Now)
                {
                    yield return new ValidationResult("Scheduled delivery date must be in the future.", new[] { nameof(ScheduledDeliveryDate) });
                }
            }

            if (DeliveryMethod == DeliveryMethod.Delivery)
            {
                var addressContext = new ValidationContext(Address, null, null);
                var addressResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(Address, addressContext, addressResults, true))
                {
                    foreach (var vr in addressResults)
                    {

                        var updatedMemberNames = vr.MemberNames.Select(m => "Address." + m).ToList();


                        if (!updatedMemberNames.Any())
                        {
                            updatedMemberNames.Add("Address");
                        }

                        yield return new ValidationResult(vr.ErrorMessage, updatedMemberNames);
                    }
                }
            }



        }
    }
}