using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Attribute
{
     public class EarlierThanNowAttribute : ValidationAttribute
     {
          protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
          {
               if (value is null)
               {
                    return new ValidationResult("Start date is invalid");
               }
               DateTime rentalStartDate = (DateTime)value;
               DateTime now = DateTime.Now;

               if (rentalStartDate >= now)
               {
                    return ValidationResult.Success;
               }

               return new ValidationResult("Start date cannot be earlier than the current date.");
          }
     }

}
