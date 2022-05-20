using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Validation
{
    public static class DtoValidationHelper
    {
        public static bool TryValidate(IValidatableObject objectToValidate, out string errorMessage)
        {
            if (objectToValidate == null)
            {
                errorMessage = new string("Given object is null");
                return false;
            }

            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(objectToValidate);
             var errors = new StringBuilder();

            if (!Validator.TryValidateObject(objectToValidate, context, validationResult, true))
            {
                foreach (var result in validationResult)
                {
                    errors.Append(result.ErrorMessage + "\n");
                }

                errorMessage = errors.ToString();
                return false;
            }

            errorMessage = "Object passed validation successfully";
            return true;
        }
    }
}
