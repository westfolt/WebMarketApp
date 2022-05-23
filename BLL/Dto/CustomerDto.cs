using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BLL.Dto
{
    public class CustomerDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DiscountPercent { get; set; }
        public ICollection<Guid> OrdersIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add(new ValidationResult("Name is null or empty", new List<string>() { "Name" }));
            if (string.IsNullOrWhiteSpace(Surname))
                errors.Add(new ValidationResult("Surname is null or empty", new List<string>() { "Surname" }));
            if (BirthDate.AddYears(18) > DateTime.Now)
                errors.Add(new ValidationResult("User must be older than 18 years", new List<string>() { "BirthDate" }));
            if (BirthDate.AddYears(100) < DateTime.Now)
                errors.Add(new ValidationResult("Wrong age entered", new List<string>() { "BirthDate" }));
            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                errors.Add(new ValidationResult("Email address not valid", new List<string>() { "Email" }));
            if (!Regex.IsMatch(Phone, @"\+[\d]{1,3}\([\d]{3}\)[\d]{7}"))
                errors.Add(new ValidationResult("Phone number is not valid", new List<string>() { "Phone" }));
            if (DiscountPercent < 0)
                errors.Add(new ValidationResult("Discount cannot be negative", new List<string>() { "DiscountPercent" }));

            return errors;
        }
    }
}
