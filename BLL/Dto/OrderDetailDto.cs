using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Dto
{
    public class OrderDetailDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal DiscountProductPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (ProductPrice <= 0)
                errors.Add(new ValidationResult("Product price must be greater than 0", new List<string>() { "ProductPrice" }));
            if (DiscountProductPrice <= 0)
                errors.Add(new ValidationResult("Discount product price must be greater than 0", new List<string>() { "DiscountProductPrice" }));

            return errors;
        }
    }
}
