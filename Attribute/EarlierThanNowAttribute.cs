using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Attribute
{
     public class EarlierThanNowAttribute : ValidationAttribute
     {
          protected override ValidationResult IsValid(object value, ValidationContext validationContext)
          {
               DateTime rentalStartDate = (DateTime)value;
               DateTime now = DateTime.Now;

               if (rentalStartDate >= now)
               {
                    return ValidationResult.Success;
               }
               else
               {
                    return new ValidationResult("Start date cannot be earlier than the current date.");
               }
          }
     }

}
