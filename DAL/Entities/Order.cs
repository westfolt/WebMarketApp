using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OperationDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum OrderStatus
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
