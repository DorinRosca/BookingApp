using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Attribute
{
     public class RentalEndDateAttribute : ValidationAttribute
     {
          protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
          {
               if (value != null)
               {

                    DateTime rentalEndDate = (DateTime)value;
                    DateTime rentalStartDate = (DateTime)(validationContext.ObjectType.GetProperty("RentalStartDate")
                         ?.GetValue(validationContext.ObjectInstance, null) ?? throw new InvalidOperationException());

                    if (rentalEndDate >= rentalStartDate)
                    {
                         return ValidationResult.Success;
                    }
                    else
                    {
                         return new ValidationResult("End date cannot be earlier than the start date.");
                    }
               }
               else return new ValidationResult("End date is invalid.");
          }
     }
}
