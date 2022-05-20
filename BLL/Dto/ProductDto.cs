using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Dto
{
    public class ProductDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Guid> OrderDetailsIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(ProductName))
                errors.Add(new ValidationResult("Product name is empty", new List<string>() { "ProductName" }));
            if (Price <= 0)
                errors.Add(new ValidationResult("Price must be greater than 0", new List<string>() { "Price" }));

            return errors;
        }
    }
}
