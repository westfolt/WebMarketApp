using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class ProductCategoryDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Guid> ProductsIds { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(CategoryName))
                errors.Add(new ValidationResult("Category name is null or empty", new List<string>() { "CategoryName" }));

            return errors;
        }
    }
}
