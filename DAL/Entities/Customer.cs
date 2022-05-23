using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Customer : BaseEntity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public int DiscountPercent { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
