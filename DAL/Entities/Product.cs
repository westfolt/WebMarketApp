using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Product : BaseEntity
    {
        public Guid ProductCategoryId { get; set; }
        public ProductCategory Category { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
