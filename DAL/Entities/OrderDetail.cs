using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderDetail:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public decimal DiscountUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
