using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class OrderDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OperationDate { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
        public ICollection<Guid> OrderDetailsIds { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (OperationDate > DateTime.Now)
                errors.Add(new ValidationResult("Illegal operation date", new List<string>() { "OperationDate" }));

            return errors;
        }
    }

    public enum OrderStatusDto
    {
        New,
        PaymentReceived,
        Sent,
        Received,
        Completed,
        CancelledByAdministrator,
        CancelledByUser,
        Undefined
    }
}
