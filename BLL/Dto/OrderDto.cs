using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OperationDate { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
        public ICollection<int> OrderDetailsIds { get; set; }
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
